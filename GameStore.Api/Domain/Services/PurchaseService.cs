using GameStore.Contracts;
using GameStore.DataAccess;
using GameStore.DataAccess.Gateways;
using GameStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// Use Cases: ชำระเงิน (Checkout), ดูประวัติการซื้อ
public class PurchaseService
{
    private readonly GameStoreContext db;
    private readonly IBankGateway bank;

    public PurchaseService(GameStoreContext db, IBankGateway bank)
    {
        this.db = db;
        this.bank = bank;
    }

    // ซื้อทุกเกมในตะกร้า
    //   1. ตรวจสอบตะกร้าและกฎต่าง ๆ
    //   2. ตัดเงินผ่านธนาคาร (จำลอง)
    //   3. สร้าง Order + OrderItems
    //   4. เพิ่มเกมเข้า Game List (library_items)
    //   5. ล้างตะกร้า
    // ขั้นตอน 2-5 อยู่ใน transaction เดียวกัน: สำเร็จทั้งหมด หรือไม่เกิดอะไรขึ้นเลย
    public async Task<PurchaseReceiptDto> CheckoutAsync(int userId, CheckoutRequest request)
    {
        await db.EnsureUserExistsAsync(userId);

        string accountNo = (request.BankAccountNo ?? "").Trim();
        if (accountNo.Length == 0)
            throw DomainException.Validation("กรุณากรอกเลขบัญชีธนาคาร");

        // ---- 1. ตรวจสอบตะกร้า ----
        var cartItems = await db.CartItems
            .Include(c => c.Game)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        if (cartItems.Count == 0)
            throw DomainException.Validation("ตะกร้าว่าง ไม่มีเกมให้ชำระเงิน");

        var delisted = cartItems.Where(c => !c.Game.IsActive).Select(c => c.Game.Title).ToList();
        if (delisted.Count > 0)
            throw DomainException.Conflict($"เกมต่อไปนี้ไม่ได้วางขายแล้ว กรุณานำออกจากตะกร้า: {string.Join(", ", delisted)}");

        var gameIds = cartItems.Select(c => c.GameId).ToList();
        var alreadyOwned = await db.LibraryItems
            .Where(l => l.UserId == userId && gameIds.Contains(l.GameId))
            .Select(l => l.Game.Title)
            .ToListAsync();
        if (alreadyOwned.Count > 0)
            throw DomainException.Conflict($"คุณมีเกมต่อไปนี้แล้ว: {string.Join(", ", alreadyOwned)}");

        decimal total = cartItems.Sum(c => c.Game.Price);
        DateTime now = DateTime.Now;

        await using var transaction = await db.Database.BeginTransactionAsync();

        // ---- 2. ตัดเงิน ----
        var payment = await bank.ChargeAsync(accountNo, total, $"GameStore: user {userId}, {cartItems.Count} game(s)");
        if (!payment.Success)
            throw DomainException.PaymentDeclined($"ธนาคารปฏิเสธการชำระเงิน: {payment.FailureReason}");
        // (ถ้า throw ตรงนี้ transaction จะถูก dispose โดยไม่ commit = rollback อัตโนมัติ)

        // ---- 3. สร้าง Order (เก็บราคา ณ วันที่ซื้อไว้ใน order_items) ----
        var order = new Order(userId, now, total, accountNo, payment.Reference!);
        foreach (var cartItem in cartItems)
        {
            var orderItem = new OrderItem(cartItem.GameId, cartItem.Game.Price);
            order.OrderItems.Add(orderItem);

            // ---- 4. เพิ่มเข้า Game List ----
            db.LibraryItems.Add(new LibraryItem(userId, cartItem.GameId, now) { OrderItem = orderItem });
        }
        db.Orders.Add(order);

        // ---- 5. ล้างตะกร้า ----
        db.CartItems.RemoveRange(cartItems);

        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        return new PurchaseReceiptDto(await GetOrderAsync(userId, order.Id), payment.BalanceAfter!.Value);
    }

    public async Task<List<OrderDto>> GetOrdersAsync(int userId)
    {
        await db.EnsureUserExistsAsync(userId);

        return await db.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderedAt)
            .Select(o => new OrderDto(
                o.Id,
                o.OrderedAt,
                o.TotalAmount,
                o.BankAccountNo,
                o.PaymentRef,
                o.OrderItems
                    .OrderBy(i => i.Id)
                    .Select(i => new OrderItemDto(i.Id, i.GameId, i.Game.Title, i.Price, i.IsRefunded))
                    .ToList()))
            .ToListAsync();
    }

    public async Task<OrderDto> GetOrderAsync(int userId, int orderId)
    {
        var order = await db.Orders
            .Where(o => o.UserId == userId && o.Id == orderId)
            .Select(o => new OrderDto(
                o.Id,
                o.OrderedAt,
                o.TotalAmount,
                o.BankAccountNo,
                o.PaymentRef,
                o.OrderItems
                    .OrderBy(i => i.Id)
                    .Select(i => new OrderItemDto(i.Id, i.GameId, i.Game.Title, i.Price, i.IsRefunded))
                    .ToList()))
            .SingleOrDefaultAsync();

        return order ?? throw DomainException.NotFound($"ไม่พบคำสั่งซื้อ id {orderId}");
    }
}

using GameStore.Api.Domain.Rules;
using GameStore.Contracts;
using GameStore.DataAccess;
using GameStore.DataAccess.Gateways;
using GameStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// Use Cases: ขอคืนเงิน (Refund), ดูประวัติการคืนเงิน
public class RefundService
{
    private readonly GameStoreContext db;
    private readonly IBankGateway bank;

    public RefundService(GameStoreContext db, IBankGateway bank)
    {
        this.db = db;
        this.bank = bank;
    }

    // ขอคืนเงิน 1 เกม (1 order item)
    //   1. ตรวจว่าเป็นรายการของผู้ใช้คนนี้จริง
    //   2. ตรวจกฎการคืนเงิน (RefundPolicy)
    //   3. คืนเงินเข้าบัญชีเดิมที่ใช้จ่าย ผ่านธนาคาร (จำลอง)
    //   4. ทำเครื่องหมายว่า order item ถูกคืนเงินแล้ว + บันทึก refund
    //   5. นำเกมออกจาก Game List
    // ขั้นตอน 3-5 อยู่ใน transaction เดียวกัน
    public async Task<RefundReceiptDto> RequestRefundAsync(int userId, RefundRequest request)
    {
        await db.EnsureUserExistsAsync(userId);

        string reason = (request.Reason ?? "").Trim();
        if (reason.Length > 500)
            throw DomainException.Validation("เหตุผลต้องไม่เกิน 500 ตัวอักษร");

        // ---- 1. ----
        var orderItem = await db.OrderItems
            .Include(i => i.Order)
            .Include(i => i.Game)
            .Include(i => i.LibraryItem)
            .SingleOrDefaultAsync(i => i.Id == request.OrderItemId && i.Order.UserId == userId)
            ?? throw DomainException.NotFound($"ไม่พบรายการสั่งซื้อ id {request.OrderItemId}");

        // ---- 2. ----
        DateTime now = DateTime.Now;
        var eligibility = RefundPolicy.Check(
            orderItem.Order.OrderedAt,
            orderItem.LibraryItem?.PlaytimeMinutes ?? 0,
            orderItem.IsRefunded,
            now);
        if (!eligibility.IsEligible)
            throw DomainException.Conflict($"ไม่สามารถคืนเงินเกม '{orderItem.Game.Title}' ได้: {eligibility.Note}");

        await using var transaction = await db.Database.BeginTransactionAsync();

        // ---- 3. ----
        var bankResult = await bank.RefundAsync(
            orderItem.Order.BankAccountNo, orderItem.Price, $"GameStore refund: order item {orderItem.Id}");
        if (!bankResult.Success)
            throw DomainException.ExternalService($"ธนาคารคืนเงินไม่สำเร็จ: {bankResult.FailureReason}");

        // ---- 4. ----
        orderItem.IsRefunded = true;
        var refund = new Refund(orderItem.Id, orderItem.Price, reason, now, bankResult.Reference!);
        db.Refunds.Add(refund);

        // ---- 5. ----
        if (orderItem.LibraryItem != null)
            db.LibraryItems.Remove(orderItem.LibraryItem);

        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        return new RefundReceiptDto(await GetRefundAsync(userId, refund.Id), bankResult.BalanceAfter!.Value);
    }

    public async Task<List<RefundDto>> GetRefundsAsync(int userId)
    {
        await db.EnsureUserExistsAsync(userId);

        return await db.Refunds
            .Where(r => r.OrderItem.Order.UserId == userId)
            .OrderByDescending(r => r.RefundedAt)
            .Select(r => new RefundDto(r.Id, r.OrderItemId, r.OrderItem.GameId, r.OrderItem.Game.Title,
                r.Amount, r.Reason, r.RefundedAt, r.PaymentRef))
            .ToListAsync();
    }

    public async Task<RefundDto> GetRefundAsync(int userId, int refundId)
    {
        var refund = await db.Refunds
            .Where(r => r.Id == refundId && r.OrderItem.Order.UserId == userId)
            .Select(r => new RefundDto(r.Id, r.OrderItemId, r.OrderItem.GameId, r.OrderItem.Game.Title,
                r.Amount, r.Reason, r.RefundedAt, r.PaymentRef))
            .SingleOrDefaultAsync();

        return refund ?? throw DomainException.NotFound($"ไม่พบรายการคืนเงิน id {refundId}");
    }
}

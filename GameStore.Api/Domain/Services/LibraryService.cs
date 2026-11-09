using GameStore.Api.Domain.Rules;
using GameStore.Contracts;
using GameStore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// Use Cases: ดู Game List, เล่นเกม (จำลอง)
public class LibraryService
{
    // กดเล่น 1 ครั้ง = จำลองว่าเล่นไป 30 นาที (ใช้ทดสอบกฎคืนเงิน "เล่นไม่ถึง 2 ชั่วโมง")
    public const int SimulatedSessionMinutes = 30;

    private readonly GameStoreContext db;

    public LibraryService(GameStoreContext db)
    {
        this.db = db;
    }

    public async Task<List<LibraryItemDto>> GetLibraryAsync(int userId)
    {
        await db.EnsureUserExistsAsync(userId);

        // ดึงเฉพาะ column ที่ต้องใช้ (Select เป็น anonymous object) แล้วค่อยคำนวณสิทธิ์คืนเงินในโปรแกรม
        // เพราะ RefundPolicy เป็นโค้ด C# ที่แปลงเป็น SQL ไม่ได้
        var rows = await db.LibraryItems
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.LastPlayedAt ?? l.AcquiredAt)
            .Select(l => new
            {
                l.GameId,
                l.Game.Title,
                l.Game.Developer,
                l.AcquiredAt,
                l.PlaytimeMinutes,
                l.LastPlayedAt,
                l.OrderItemId,
                l.OrderItem.Price,
                l.OrderItem.IsRefunded,
                l.OrderItem.Order.OrderedAt,
            })
            .ToListAsync();

        DateTime now = DateTime.Now;
        return rows.Select(r =>
        {
            var eligibility = RefundPolicy.Check(r.OrderedAt, r.PlaytimeMinutes, r.IsRefunded, now);
            return new LibraryItemDto(
                r.GameId, r.Title, r.Developer, r.AcquiredAt, r.PlaytimeMinutes, r.LastPlayedAt,
                r.OrderItemId, r.Price, eligibility.IsEligible, eligibility.Note);
        }).ToList();
    }

    public async Task<LibraryItemDto> PlayAsync(int userId, int gameId)
    {
        var item = await db.LibraryItems.SingleOrDefaultAsync(l => l.UserId == userId && l.GameId == gameId)
            ?? throw DomainException.NotFound("ไม่พบเกมนี้ใน Game List");

        item.PlaytimeMinutes += SimulatedSessionMinutes;
        item.LastPlayedAt = DateTime.Now;
        await db.SaveChangesAsync();

        var library = await GetLibraryAsync(userId);
        return library.Single(l => l.GameId == gameId);
    }
}

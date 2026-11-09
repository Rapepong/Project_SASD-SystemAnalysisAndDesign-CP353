using GameStore.Contracts;
using GameStore.DataAccess;
using GameStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// Use Cases: เพิ่มเกมลงตะกร้า, นำเกมออกจากตะกร้า, ดูตะกร้า
public class CartService
{
    private readonly GameStoreContext db;

    public CartService(GameStoreContext db)
    {
        this.db = db;
    }

    public async Task<CartDto> GetCartAsync(int userId)
    {
        await db.EnsureUserExistsAsync(userId);

        var items = await db.CartItems
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.AddedAt)
            .Select(c => new CartItemDto(c.GameId, c.Game.Title, c.Game.Price, c.AddedAt))
            .ToListAsync();

        return new CartDto(items, items.Sum(i => i.Price));
    }

    public async Task<CartDto> AddItemAsync(int userId, int gameId)
    {
        await db.EnsureUserExistsAsync(userId);

        var game = await db.Games.SingleOrDefaultAsync(g => g.Id == gameId)
            ?? throw DomainException.NotFound($"ไม่พบเกม id {gameId}");

        // ---- กฎทาง business ----
        if (!game.IsActive)
            throw DomainException.Conflict($"เกม '{game.Title}' ไม่ได้วางขายแล้ว");

        if (await db.LibraryItems.AnyAsync(l => l.UserId == userId && l.GameId == gameId))
            throw DomainException.Conflict($"คุณมีเกม '{game.Title}' ใน Game List แล้ว");

        if (await db.CartItems.AnyAsync(c => c.UserId == userId && c.GameId == gameId))
            throw DomainException.Conflict($"เกม '{game.Title}' อยู่ในตะกร้าแล้ว");

        db.CartItems.Add(new CartItem(userId, gameId, DateTime.Now));
        await db.SaveChangesAsync();

        return await GetCartAsync(userId);
    }

    public async Task RemoveItemAsync(int userId, int gameId)
    {
        var item = await db.CartItems.SingleOrDefaultAsync(c => c.UserId == userId && c.GameId == gameId)
            ?? throw DomainException.NotFound("ไม่พบเกมนี้ในตะกร้า");

        db.CartItems.Remove(item);
        await db.SaveChangesAsync();
    }
}

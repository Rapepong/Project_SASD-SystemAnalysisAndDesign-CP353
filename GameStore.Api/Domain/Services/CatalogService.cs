using GameStore.Contracts;
using GameStore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// Use Cases: ค้นหาเกม (ตามชื่อ / ตาม genre), ดูรายละเอียดเกม
public class CatalogService
{
    private readonly GameStoreContext db;

    public CatalogService(GameStoreContext db)
    {
        this.db = db;
    }

    public async Task<List<GenreDto>> GetGenresAsync()
    {
        return await db.Genres
            .OrderBy(g => g.Name)
            .Select(g => new GenreDto(g.Id, g.Name))
            .ToListAsync();
    }

    // ค้นหาเกมที่วางขายอยู่ (is_active = true)
    //   search  : ค้นจากชื่อเกม (ไม่สนตัวพิมพ์เล็ก/ใหญ่)
    //   genreId : กรองตามแนวเกม
    // LINQ ทั้งก้อนจะถูกแปลงเป็น SQL คำสั่งเดียว แล้วให้ database กรองให้ (ไม่ได้โหลดทั้ง table มากรองในโปรแกรม)
    public async Task<List<GameSummaryDto>> SearchGamesAsync(string? search, int? genreId)
    {
        var query = db.Games.Where(g => g.IsActive);

        if (!string.IsNullOrWhiteSpace(search))
        {
            string pattern = "%" + EscapeLikePattern(search.Trim()) + "%";
            query = query.Where(g => EF.Functions.ILike(g.Title, pattern, @"\"));
        }

        if (genreId != null)
        {
            query = query.Where(g => g.Genres.Any(genre => genre.Id == genreId));
        }

        return await query
            .OrderBy(g => g.Title)
            .Select(g => new GameSummaryDto(
                g.Id,
                g.Title,
                g.Developer,
                g.Price,
                g.Genres.OrderBy(genre => genre.Name).Select(genre => genre.Name).ToList()))
            .ToListAsync();
    }

    public async Task<GameDetailDto> GetGameAsync(int gameId)
    {
        var game = await db.Games
            .Where(g => g.Id == gameId && g.IsActive)
            .Select(g => new GameDetailDto(
                g.Id,
                g.Title,
                g.Developer,
                g.Description,
                g.Price,
                g.ReleaseDate,
                g.Genres.OrderBy(genre => genre.Name).Select(genre => genre.Name).ToList()))
            .SingleOrDefaultAsync();

        return game ?? throw DomainException.NotFound($"ไม่พบเกม id {gameId}");
    }

    // ป้องกันไม่ให้ตัวอักษร % และ _ ที่ผู้ใช้พิมพ์ ถูกตีความเป็น wildcard ของ LIKE
    private static string EscapeLikePattern(string text) =>
        text.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_");
}

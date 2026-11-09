using GameStore.Api.Domain.Services;
using GameStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/v1/")]
public class CatalogController : ControllerBase
{
    private readonly CatalogService catalog;

    public CatalogController(CatalogService catalog)
    {
        this.catalog = catalog;
    }

    // GET /api/v1/genres
    [HttpGet("genres")]
    public async Task<List<GenreDto>> GetGenres()
    {
        return await catalog.GetGenresAsync();
    }

    // GET /api/v1/games                       ทุกเกมที่วางขาย
    // GET /api/v1/games?search=star            ค้นหาตามชื่อ  (ใช้ query parameter ในการ filter)
    // GET /api/v1/games?genreId=3              กรองตาม genre
    // GET /api/v1/games?search=s&genreId=1     ใช้ร่วมกันได้
    [HttpGet("games")]
    public async Task<List<GameSummaryDto>> GetGames([FromQuery] string? search, [FromQuery] int? genreId)
    {
        return await catalog.SearchGamesAsync(search, genreId);
    }

    // GET /api/v1/games/3
    [HttpGet("games/{gameId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GameDetailDto> GetGame(int gameId)
    {
        return await catalog.GetGameAsync(gameId);
    }
}

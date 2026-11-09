using GameStore.Api.Domain.Services;
using GameStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

// Game List ของลูกค้า  ->  /users/{userId}/library
[ApiController]
[Route("api/v1/")]
public class LibraryController : ControllerBase
{
    private readonly LibraryService library;

    public LibraryController(LibraryService library)
    {
        this.library = library;
    }

    // GET /api/v1/users/1/library
    [HttpGet("users/{userId:int}/library")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<LibraryItemDto>> GetLibrary(int userId)
    {
        return await library.GetLibraryAsync(userId);
    }

    // POST /api/v1/users/1/library/3:play     เล่นเกม (จำลอง +30 นาที)
    // HTTP verbs มาตรฐานไม่มีคำว่า "เล่น" จึงใช้ custom method แบบ Google  (ชีต 12 หน้า 24)
    [HttpPost("users/{userId:int}/library/{gameId:int}:play")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<LibraryItemDto> Play(int userId, int gameId)
    {
        return await library.PlayAsync(userId, gameId);
    }
}

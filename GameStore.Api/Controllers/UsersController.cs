using GameStore.Api.Domain.Services;
using GameStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

// Controller = รับ HTTP Request แต่ละ end point แล้วส่งต่อให้ Domain Service ทำงาน
// (Controller บาง ๆ ไม่มี business logic อยู่ในนี้)
[ApiController]
[Route("api/v1/")]
public class UsersController : ControllerBase
{
    private readonly AccountService accounts;

    public UsersController(AccountService accounts)
    {
        this.accounts = accounts;
    }

    // POST /api/v1/users           สมัครสมาชิก -> 201 Created
    [HttpPost("users")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> Register(RegisterRequest request)
    {
        UserDto user = await accounts.RegisterAsync(request);
        return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
    }

    // POST /api/v1/users:login     เข้าสู่ระบบ (custom method แบบที่ Google ใช้ ตามชีต 12 หน้า 24)
    [HttpPost("users:login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<UserDto> Login(LoginRequest request)
    {
        return await accounts.LoginAsync(request);
    }

    // GET /api/v1/users/5
    [HttpGet("users/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserDto> GetUser(int userId)
    {
        return await accounts.GetUserAsync(userId);
    }
}

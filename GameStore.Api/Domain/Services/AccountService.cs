using System.Text.RegularExpressions;
using GameStore.Api.Domain.Rules;
using GameStore.Contracts;
using GameStore.DataAccess;
using GameStore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// Use Cases: สมัครสมาชิก (Register), เข้าสู่ระบบ (Login)
public class AccountService
{
    private readonly GameStoreContext db;

    public AccountService(GameStoreContext db)
    {
        this.db = db;
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest request)
    {
        string username = (request.Username ?? "").Trim().ToLowerInvariant();
        string password = request.Password ?? "";
        string displayName = (request.DisplayName ?? "").Trim();
        string email = (request.Email ?? "").Trim();

        // ---- ตรวจสอบข้อมูล (Validation) ----
        if (!Regex.IsMatch(username, "^[a-z0-9_]{3,50}$"))
            throw DomainException.Validation("Username ต้องเป็น a-z, 0-9 หรือ _ ยาว 3-50 ตัวอักษร");
        if (password.Length < 6)
            throw DomainException.Validation("รหัสผ่านต้องยาวอย่างน้อย 6 ตัวอักษร");
        if (displayName.Length is 0 or > 100)
            throw DomainException.Validation("กรุณากรอกชื่อที่แสดง (ไม่เกิน 100 ตัวอักษร)");
        if (!email.Contains('@') || email.Length > 100)
            throw DomainException.Validation("รูปแบบอีเมลไม่ถูกต้อง");

        // ---- กฎ: username ห้ามซ้ำ ----
        if (await db.Users.AnyAsync(u => u.Username == username))
            throw DomainException.Conflict($"Username '{username}' ถูกใช้แล้ว");

        var user = new User(username, PasswordHasher.Hash(password), displayName, email, DateTime.Now);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        return ToDto(user);
    }

    public async Task<UserDto> LoginAsync(LoginRequest request)
    {
        string username = (request.Username ?? "").Trim().ToLowerInvariant();
        var user = await db.Users.SingleOrDefaultAsync(u => u.Username == username);

        // ไม่บอกว่าผิดที่ username หรือ password เพื่อไม่ให้เดา username ได้
        if (user == null || !PasswordHasher.Verify(request.Password ?? "", user.PasswordHash))
            throw DomainException.Unauthorized("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");

        return ToDto(user);
    }

    public async Task<UserDto> GetUserAsync(int userId)
    {
        var user = await db.Users.SingleOrDefaultAsync(u => u.Id == userId)
            ?? throw DomainException.NotFound($"ไม่พบผู้ใช้ id {userId}");
        return ToDto(user);
    }

    private static UserDto ToDto(User user) =>
        new(user.Id, user.Username, user.DisplayName, user.Email);
}

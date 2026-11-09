using GameStore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Domain.Services;

// การตรวจสอบที่หลาย service ใช้ร่วมกัน
public static class DbChecks
{
    public static async Task EnsureUserExistsAsync(this GameStoreContext db, int userId)
    {
        bool exists = await db.Users.AnyAsync(u => u.Id == userId);
        if (!exists)
            throw DomainException.NotFound($"ไม่พบผู้ใช้ id {userId}");
    }
}

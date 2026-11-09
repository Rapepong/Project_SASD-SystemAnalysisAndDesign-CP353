using GameStore.Contracts;

namespace GameStore.Client.Services;

// ข้อมูลของผู้ใช้ที่ login อยู่ (เก็บไว้ในฝั่ง Client)
public static class Session
{
    public static UserDto? CurrentUser { get; set; }

    public static int UserId => CurrentUser?.Id
        ?? throw new InvalidOperationException("ยังไม่ได้ login");

    // จำเลขบัญชีล่าสุดที่ใช้จ่ายเงิน จะได้ไม่ต้องพิมพ์ใหม่
    public static string? LastBankAccountNo { get; set; }
}

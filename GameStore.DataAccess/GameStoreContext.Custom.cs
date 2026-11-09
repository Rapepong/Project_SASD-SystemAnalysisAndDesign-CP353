using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess;

// ส่วนที่เราเขียนเพิ่มเองให้ GameStoreContext (partial class)
// แยกไฟล์ไว้ เพื่อให้ scaffold ใหม่ด้วย --force ได้โดยโค้ดส่วนนี้ไม่ถูกเขียนทับ
//
// มี 2 วิธีสร้าง context
//   1. new GameStoreContext(connectionString)          <- แบบในชีต 11 (เช่นใช้ใน console / test)
//   2. ASP.Net สร้างให้ผ่าน Dependency Injection        <- ดู Program.cs ของ GameStore.Api
public partial class GameStoreContext
{
    private readonly string? connectionString;

    public GameStoreContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // ถ้าถูกตั้งค่ามาจาก DI แล้ว (IsConfigured) ไม่ต้องตั้งซ้ำ
        if (!optionsBuilder.IsConfigured && connectionString != null)
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
    }
}

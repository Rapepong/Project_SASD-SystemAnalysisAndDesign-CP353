using GameStore.Api.Domain.Services;
using GameStore.Api.ErrorHandling;
using GameStore.DataAccess;
using GameStore.DataAccess.Gateways;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------- Data Access Layer ----------------
// connection string อยู่ใน appsettings.json (แก้ได้โดยไม่ต้องแก้ source code)
// Client ไม่เคยเห็น connection string นี้ เพราะอยู่ฝั่ง Server Tier เท่านั้น (ข้อดีของ 3-Tier)
string connectionString = builder.Configuration.GetConnectionString("GameStore")
    ?? throw new InvalidOperationException("ไม่พบ ConnectionStrings:GameStore ใน appsettings.json");

builder.Services.AddDbContext<GameStoreContext>(options => options
    .UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention());

// ธนาคารจำลอง — ถ้าต่อธนาคารจริงในอนาคต เปลี่ยนแค่บรรทัดนี้
builder.Services.AddScoped<IBankGateway, SimulatedBankGateway>();

// ---------------- Domain Layer ----------------
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<LibraryService>();
builder.Services.AddScoped<RefundService>();

// ---------------- REST API ----------------
builder.Services.AddControllers();
builder.Services.AddOpenApi();                 // สร้าง /openapi/v1.json ให้อัตโนมัติ
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();

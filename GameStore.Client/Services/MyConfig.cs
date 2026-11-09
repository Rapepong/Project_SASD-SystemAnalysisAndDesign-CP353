using Microsoft.Extensions.Configuration;

namespace GameStore.Client.Services;

// อ่านค่า config จาก appsettings.json (แบบในชีต 11-12)
// จะ connect หา Server ใด ขึ้นกับค่า BaseUri ในไฟล์นั้น
public static class MyConfig
{
    private static readonly Lazy<string> baseUri = new(ReadBaseUri);

    public static string BaseUri => baseUri.Value;

    private static string ReadBaseUri()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        return config["BaseUri"]
            ?? throw new InvalidOperationException("ไม่พบค่า BaseUri ใน appsettings.json");
    }
}

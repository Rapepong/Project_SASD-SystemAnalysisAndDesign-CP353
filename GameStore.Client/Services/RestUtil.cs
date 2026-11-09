using System.Net.Http.Json;
using System.Text.Json;

namespace GameStore.Client.Services;

// ตัวช่วย call REST API (wrap HttpClient แบบ RestUtil ในชีต 12)
// ต่างจากในชีตตรงที่
//   1. เป็นแบบ async  -> หน้าจอไม่ค้าง (hang) ระหว่างรอ server
//   2. ดัก Exception -> ถ้า server down จะแจ้งผู้ใช้ แทนที่โปรแกรมจะค้าง (ตาม Todo ท้ายชีต 12)
//   3. อ่าน error message (Problem Details) ที่ server ส่งกลับมา แล้วโยนเป็น ApiException
public static class RestUtil
{
    private static readonly HttpClient client = new() { Timeout = TimeSpan.FromSeconds(10) };

    // camelCase แบบเดียวกับที่ ASP.Net ใช้
    private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);

    public static Task<T> Get<T>(string baseUri, string path) =>
        Send<T>(HttpMethod.Get, baseUri, path, null);

    public static Task<T> Post<T>(string baseUri, string path, object? body) =>
        Send<T>(HttpMethod.Post, baseUri, path, body);

    public static async Task Delete(string baseUri, string path) =>
        await Send<object?>(HttpMethod.Delete, baseUri, path, null);

    private static async Task<T> Send<T>(HttpMethod method, string baseUri, string path, object? body)
    {
        // ต่อ string เอง ไม่ใช้ new Uri(base, path)
        // เพราะ path แบบ "users:login" จะถูกตีความผิดเป็น URI ที่มี scheme ชื่อ "users"
        string url = baseUri.TrimEnd('/') + "/" + path.TrimStart('/');

        using var request = new HttpRequestMessage(method, url);
        if (body != null)
            request.Content = JsonContent.Create(body, body.GetType(), options: jsonOptions);

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(request);
        }
        catch (HttpRequestException)
        {
            throw new ApiException(0, $"เชื่อมต่อ server ไม่ได้ ({url})\nตรวจสอบว่า run GameStore.Api แล้ว และ BaseUri ใน appsettings.json ถูกต้อง");
        }
        catch (TaskCanceledException)
        {
            throw new ApiException(0, "server ไม่ตอบสนอง (time-out) กรุณาลองใหม่");
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApiException((int)response.StatusCode, await ReadErrorMessage(response));

            // 204 No Content ไม่มี body
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent || typeof(T) == typeof(object))
                return default!;

            T? result = await response.Content.ReadFromJsonAsync<T>(jsonOptions);
            return result ?? throw new ApiException((int)response.StatusCode, "server ส่งข้อมูลกลับมาว่างเปล่า");
        }
    }

    // อ่าน body แบบ Problem Details เช่น { "title": "Conflict", "status": 409, "detail": "..." }
    private static async Task<string> ReadErrorMessage(HttpResponseMessage response)
    {
        try
        {
            var problem = await response.Content.ReadFromJsonAsync<ProblemBody>(jsonOptions);
            if (!string.IsNullOrWhiteSpace(problem?.Detail))
                return problem.Detail;
            if (problem?.Errors is { Count: > 0 })
                return string.Join("\n", problem.Errors.SelectMany(e => e.Value));
            if (!string.IsNullOrWhiteSpace(problem?.Title))
                return problem.Title;
        }
        catch (JsonException)
        {
            // body ไม่ใช่ JSON
        }
        return $"เกิดข้อผิดพลาด (HTTP {(int)response.StatusCode} {response.ReasonPhrase})";
    }

    private record ProblemBody(string? Title, string? Detail, Dictionary<string, string[]>? Errors);
}

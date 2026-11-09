namespace GameStore.Client.Services;

// เกิดเมื่อเรียก REST API แล้วไม่สำเร็จ
// StatusCode = 0 หมายถึงติดต่อ server ไม่ได้เลย (server down / time-out)
public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

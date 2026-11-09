namespace GameStore.Api.Domain;

// ประเภทของความผิดพลาดทาง business (จะถูกแปลงเป็น HTTP Status Code ใน DomainExceptionHandler)
public enum DomainErrorKind
{
    Validation,       // 400 Bad Request       : ข้อมูลที่ส่งมาไม่ถูกต้อง
    Unauthorized,     // 401 Unauthorized      : login ไม่ผ่าน
    PaymentDeclined,  // 402 Payment Required  : ธนาคารปฏิเสธการตัดเงิน
    NotFound,         // 404 Not Found         : ไม่พบข้อมูล
    Conflict,         // 409 Conflict          : ขัดกับสถานะปัจจุบัน เช่น มีเกมนี้แล้ว, เลยกำหนดคืนเงิน
    ExternalService,  // 502 Bad Gateway       : ระบบภายนอก (ธนาคาร) ทำงานไม่สำเร็จ
}

// Domain Layer จะ throw exception นี้เมื่อผิดกฎทาง business
// ทำให้ Domain ไม่ต้องรู้จัก HTTP เลย (แยกหน้าที่ระหว่าง Domain กับ REST API)
public class DomainException : Exception
{
    public DomainErrorKind Kind { get; }

    public DomainException(DomainErrorKind kind, string message) : base(message)
    {
        Kind = kind;
    }

    public static DomainException Validation(string message) => new(DomainErrorKind.Validation, message);
    public static DomainException Unauthorized(string message) => new(DomainErrorKind.Unauthorized, message);
    public static DomainException PaymentDeclined(string message) => new(DomainErrorKind.PaymentDeclined, message);
    public static DomainException NotFound(string message) => new(DomainErrorKind.NotFound, message);
    public static DomainException Conflict(string message) => new(DomainErrorKind.Conflict, message);
    public static DomainException ExternalService(string message) => new(DomainErrorKind.ExternalService, message);
}

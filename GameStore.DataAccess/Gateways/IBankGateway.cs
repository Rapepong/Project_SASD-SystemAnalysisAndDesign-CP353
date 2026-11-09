namespace GameStore.DataAccess.Gateways;

// Gateway ไปยังระบบธนาคาร (ระบบภายนอก)
// Domain Layer เรียกใช้ผ่าน interface นี้เท่านั้น จึงไม่รู้ว่าข้างหลังเป็นธนาคารจริงหรือธนาคารจำลอง
// ถ้าวันหนึ่งต้องต่อธนาคารจริง ก็แค่เขียนคลาสใหม่ที่ implement interface นี้ แล้วเปลี่ยนใน Program.cs
public interface IBankGateway
{
    // ตัดเงินจากบัญชีลูกค้า
    Task<BankTransactionResult> ChargeAsync(string accountNo, decimal amount, string description);

    // คืนเงินเข้าบัญชีลูกค้า
    Task<BankTransactionResult> RefundAsync(string accountNo, decimal amount, string description);
}

// ผลลัพธ์ที่ธนาคารตอบกลับมา (เหมือน slip)
public record BankTransactionResult(
    bool Success,
    string? Reference,
    decimal? BalanceAfter,
    string? FailureReason)
{
    public static BankTransactionResult Ok(string reference, decimal balanceAfter) =>
        new(true, reference, balanceAfter, null);

    public static BankTransactionResult Fail(string reason) =>
        new(false, null, null, reason);
}

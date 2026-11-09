using Microsoft.EntityFrameworkCore;

namespace GameStore.DataAccess.Gateways;

// ธนาคารจำลอง (ตามโจทย์: จำลองการตัดเงินจากธนาคาร ไม่ต้องเชื่อมต่อจริง)
// เก็บยอดเงินไว้ใน table bank_accounts
//
// ใช้ GameStoreContext ตัวเดียวกับ Domain Service (scoped ต่อ 1 request)
// ดังนั้นการตัดเงินจะอยู่ใน transaction เดียวกับการสร้าง order
// ถ้าขั้นตอนไหนล้มเหลว จะ rollback ทั้งหมด (ไม่มีกรณีตัดเงินแล้วไม่ได้เกม)
public class SimulatedBankGateway : IBankGateway
{
    private readonly GameStoreContext db;

    public SimulatedBankGateway(GameStoreContext db)
    {
        this.db = db;
    }

    public async Task<BankTransactionResult> ChargeAsync(string accountNo, decimal amount, string description)
    {
        if (amount <= 0)
            return BankTransactionResult.Fail("จำนวนเงินไม่ถูกต้อง");

        bool exists = await db.BankAccounts.AnyAsync(a => a.AccountNo == accountNo);
        if (!exists)
            return BankTransactionResult.Fail($"ไม่พบบัญชีธนาคารเลขที่ {accountNo}");

        // UPDATE bank_accounts SET balance = balance - @amount
        // WHERE account_no = @accountNo AND balance >= @amount
        // ทำในคำสั่งเดียว (atomic) จึงไม่มีทางตัดเงินจนติดลบ แม้มีหลาย request เข้ามาพร้อมกัน
        int updatedRows = await db.BankAccounts
            .Where(a => a.AccountNo == accountNo && a.Balance >= amount)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.Balance, a => a.Balance - amount));

        if (updatedRows == 0)
            return BankTransactionResult.Fail("ยอดเงินในบัญชีไม่เพียงพอ");

        return BankTransactionResult.Ok(NewReference("PAY"), await GetBalanceAsync(accountNo));
    }

    public async Task<BankTransactionResult> RefundAsync(string accountNo, decimal amount, string description)
    {
        if (amount <= 0)
            return BankTransactionResult.Fail("จำนวนเงินไม่ถูกต้อง");

        int updatedRows = await db.BankAccounts
            .Where(a => a.AccountNo == accountNo)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.Balance, a => a.Balance + amount));

        if (updatedRows == 0)
            return BankTransactionResult.Fail($"ไม่พบบัญชีธนาคารเลขที่ {accountNo}");

        return BankTransactionResult.Ok(NewReference("RFD"), await GetBalanceAsync(accountNo));
    }

    private Task<decimal> GetBalanceAsync(string accountNo) =>
        db.BankAccounts
            .Where(a => a.AccountNo == accountNo)
            .Select(a => a.Balance)
            .SingleAsync();

    // เลขอ้างอิงรายการ เช่น PAY-20261108153012-4821
    private static string NewReference(string prefix) =>
        $"{prefix}-{DateTime.Now:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}";
}

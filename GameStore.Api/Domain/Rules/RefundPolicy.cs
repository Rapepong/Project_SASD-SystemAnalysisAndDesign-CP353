using System.Globalization;

namespace GameStore.Api.Domain.Rules;

// ผลการตรวจสอบสิทธิ์การคืนเงิน
public record RefundEligibility(bool IsEligible, string Note);

// กฎการคืนเงิน (Business Rule) — อ้างอิงแนวทางของ Steam
//   1. ขอคืนเงินได้ภายใน 14 วันหลังซื้อ
//   2. เล่นไปแล้วต้องไม่ถึง 2 ชั่วโมง
//   3. รายการเดิมคืนเงินได้ครั้งเดียว
//
// เป็น pure function (ไม่ยุ่งกับ database) จึงเขียน unit test ได้ง่าย -> ดู GameStore.Tests
public static class RefundPolicy
{
    public const int MaxDaysAfterPurchase = 14;
    public const int MaxPlaytimeMinutes = 120;

    public static RefundEligibility Check(DateTime orderedAt, int playtimeMinutes, bool isRefunded, DateTime now)
    {
        if (isRefunded)
            return new RefundEligibility(false, "คืนเงินรายการนี้ไปแล้ว");

        DateTime deadline = orderedAt.AddDays(MaxDaysAfterPurchase);
        string deadlineText = deadline.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

        if (now > deadline)
            return new RefundEligibility(false,
                $"เกิน {MaxDaysAfterPurchase} วันหลังซื้อแล้ว (ครบกำหนดเมื่อ {deadlineText})");

        if (playtimeMinutes >= MaxPlaytimeMinutes)
            return new RefundEligibility(false,
                $"เล่นไปแล้ว {FormatPlaytime(playtimeMinutes)} (ต้องเล่นไม่ถึง {MaxPlaytimeMinutes / 60} ชั่วโมง)");

        return new RefundEligibility(true, $"ขอคืนเงินได้ถึง {deadlineText}");
    }

    public static string FormatPlaytime(int minutes) =>
        minutes >= 60 ? $"{minutes / 60} ชม. {minutes % 60} นาที" : $"{minutes} นาที";
}

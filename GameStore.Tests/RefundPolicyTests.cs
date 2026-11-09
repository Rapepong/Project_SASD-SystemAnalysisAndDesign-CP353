using GameStore.Api.Domain.Rules;

namespace GameStore.Tests;

// ทดสอบกฎการคืนเงิน: ภายใน 14 วัน และเล่นไม่ถึง 2 ชั่วโมง
public class RefundPolicyTests
{
    private static readonly DateTime OrderedAt = new(2026, 11, 1, 10, 0, 0);

    [Fact]
    public void NewPurchase_NotPlayed_IsEligible()
    {
        var result = RefundPolicy.Check(OrderedAt, playtimeMinutes: 0, isRefunded: false, now: OrderedAt.AddHours(1));

        Assert.True(result.IsEligible);
    }

    [Fact]
    public void ExactlyFourteenDays_IsStillEligible()
    {
        var result = RefundPolicy.Check(OrderedAt, 0, false, now: OrderedAt.AddDays(14));

        Assert.True(result.IsEligible);
    }

    [Fact]
    public void MoreThanFourteenDays_IsNotEligible()
    {
        var result = RefundPolicy.Check(OrderedAt, 0, false, now: OrderedAt.AddDays(14).AddMinutes(1));

        Assert.False(result.IsEligible);
        Assert.Contains("14 วัน", result.Note);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(119, true)]
    [InlineData(120, false)]   // เล่นครบ 2 ชั่วโมงพอดี = คืนไม่ได้
    [InlineData(600, false)]
    public void Playtime_MustBeLessThanTwoHours(int playtimeMinutes, bool expected)
    {
        var result = RefundPolicy.Check(OrderedAt, playtimeMinutes, false, now: OrderedAt.AddDays(1));

        Assert.Equal(expected, result.IsEligible);
    }

    [Fact]
    public void AlreadyRefunded_IsNotEligible()
    {
        var result = RefundPolicy.Check(OrderedAt, 0, isRefunded: true, now: OrderedAt.AddHours(1));

        Assert.False(result.IsEligible);
    }
}

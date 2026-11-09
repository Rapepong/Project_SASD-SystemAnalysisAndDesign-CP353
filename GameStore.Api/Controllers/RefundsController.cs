using GameStore.Api.Domain.Services;
using GameStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/v1/")]
public class RefundsController : ControllerBase
{
    private readonly RefundService refunds;

    public RefundsController(RefundService refunds)
    {
        this.refunds = refunds;
    }

    // POST /api/v1/users/1/refunds    body: { "orderItemId": 4, "reason": "เครื่องไม่แรงพอ" }
    // ขอคืนเงิน = สร้าง refund ใหม่ -> 201 Created
    // ถ้าผิดเงื่อนไข (เกิน 14 วัน / เล่นเกิน 2 ชม. / คืนไปแล้ว) -> 409 Conflict
    [HttpPost("users/{userId:int}/refunds")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RefundReceiptDto>> RequestRefund(int userId, RefundRequest request)
    {
        RefundReceiptDto receipt = await refunds.RequestRefundAsync(userId, request);
        return CreatedAtAction(nameof(GetRefund), new { userId, refundId = receipt.Refund.Id }, receipt);
    }

    // GET /api/v1/users/1/refunds     ประวัติการคืนเงิน
    [HttpGet("users/{userId:int}/refunds")]
    public async Task<List<RefundDto>> GetRefunds(int userId)
    {
        return await refunds.GetRefundsAsync(userId);
    }

    // GET /api/v1/users/1/refunds/2
    [HttpGet("users/{userId:int}/refunds/{refundId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<RefundDto> GetRefund(int userId, int refundId)
    {
        return await refunds.GetRefundAsync(userId, refundId);
    }
}

using GameStore.Api.Domain.Services;
using GameStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/v1/")]
public class OrdersController : ControllerBase
{
    private readonly PurchaseService purchases;

    public OrdersController(PurchaseService purchases)
    {
        this.purchases = purchases;
    }

    // POST /api/v1/users/1/orders    body: { "bankAccountNo": "111-1-11111-1" }
    // ชำระเงิน = สร้าง order ใหม่จากของในตะกร้า -> 201 Created
    // ถ้าเงินไม่พอ -> 402 Payment Required
    [HttpPost("users/{userId:int}/orders")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PurchaseReceiptDto>> Checkout(int userId, CheckoutRequest request)
    {
        PurchaseReceiptDto receipt = await purchases.CheckoutAsync(userId, request);
        return CreatedAtAction(nameof(GetOrder), new { userId, orderId = receipt.Order.Id }, receipt);
    }

    // GET /api/v1/users/1/orders     ประวัติการซื้อ
    [HttpGet("users/{userId:int}/orders")]
    public async Task<List<OrderDto>> GetOrders(int userId)
    {
        return await purchases.GetOrdersAsync(userId);
    }

    // GET /api/v1/users/1/orders/7
    [HttpGet("users/{userId:int}/orders/{orderId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<OrderDto> GetOrder(int userId, int orderId)
    {
        return await purchases.GetOrderAsync(userId, orderId);
    }
}

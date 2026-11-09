using GameStore.Api.Domain.Services;
using GameStore.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

// ตะกร้าเป็น sub-resource ของ user  ->  /users/{userId}/cart
[ApiController]
[Route("api/v1/")]
public class CartController : ControllerBase
{
    private readonly CartService cart;

    public CartController(CartService cart)
    {
        this.cart = cart;
    }

    // GET /api/v1/users/1/cart
    [HttpGet("users/{userId:int}/cart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CartDto> GetCart(int userId)
    {
        return await cart.GetCartAsync(userId);
    }

    // POST /api/v1/users/1/cart/items    body: { "gameId": 3 }   -> 201 Created
    [HttpPost("users/{userId:int}/cart/items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CartDto>> AddItem(int userId, AddCartItemRequest request)
    {
        CartDto result = await cart.AddItemAsync(userId, request.GameId);
        return CreatedAtAction(nameof(GetCart), new { userId }, result);
    }

    // DELETE /api/v1/users/1/cart/items/3   -> 204 No Content
    [HttpDelete("users/{userId:int}/cart/items/{gameId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItem(int userId, int gameId)
    {
        await cart.RemoveItemAsync(userId, gameId);
        return NoContent();
    }
}

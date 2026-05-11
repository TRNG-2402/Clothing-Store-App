using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Exceptions;

namespace CommerceSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


[HttpGet]
public async Task<ActionResult<CartItemDTO>> GetCartItems()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId == null)
        return Unauthorized();

    var cartItems = await _cartService.GetCartItems(userId);

    return Ok(cartItems);
}



[HttpPost("items")]
public async Task<IActionResult> AddItem(AddCartItemDto dto)
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    await _cartService.InsertItem(userId, dto.ProductId, dto.Quantity);

    return Ok();
}




[HttpPut("items")]
public async Task<IActionResult> UpdateItem(UpdateCartItemDto dto)
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    await _cartService.UpdateQuantity(userId, dto.ProductId, dto.Quantity);

    return Ok();
}




[HttpDelete("items/{productId}")]
public async Task<IActionResult> DeleteItem(int productId)
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    await _cartService.DeleteItem(userId, productId);

    return Ok();
}
    

}
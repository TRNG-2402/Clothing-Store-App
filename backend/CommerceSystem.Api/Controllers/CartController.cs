using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.DTOs;

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

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = GetUserId();

        var cart = await _cartService.GetCartByUserIdAsync(userId);

        return Ok(cart);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest request)
    {
        var userId = GetUserId();

        var cart = await _cartService.AddItemAsync(userId, request);

        return Ok(cart);
    }

    [HttpPatch("items/{productId}")]
    public async Task<IActionResult> UpdateItemQuantity(
        int productId,
        [FromBody] UpdateCartItemRequest request)
    {
        var userId = GetUserId();

        var cart = await _cartService.UpdateItemQuantityAsync(userId, productId, request);

        return Ok(cart);
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> RemoveItem(int productId)
    {
        var userId = GetUserId();

        await _cartService.RemoveItemAsync(userId, productId);

        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ClearCart()
    {
        var userId = GetUserId();

        await _cartService.ClearCartAsync(userId);

        return NoContent();
    }
}
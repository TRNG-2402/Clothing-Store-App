using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.Exceptions;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Controllers;

[Authorize] // user must be logged in to view cart
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // api/cart/{userId}
    // Returns the cart and all its items for a given user
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCartByUserId(int userId)
    {
        try
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // api/cart/{userId}/items
    // Adds a product to the user's cart
    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddItem(int userId, [FromBody] AddCartItemRequest request)
    {
        try
        {
            var cart = await _cartService.AddItemAsync(userId, request);
            return Ok(cart);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ProductNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // api/cart/{userId}/items/{productId}
    // Updates the quantity of a specific item in user's cart
    [HttpPatch("{userId}/items/{productId}")]
    public async Task<IActionResult> UpdateItemQuantity(int userId, int productId, [FromBody] UpdateCartItemRequest request)
    {
        try
        {
            var cart = await _cartService.UpdateItemQuantityAsync(userId, productId, request);
            return Ok(cart);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InsufficientStockException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ProductNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // api/cart/{userId}/items/{productId}
    // Removes specified item from the cart
    [HttpDelete("{userId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(int userId, int productId)
    {
        try
        {
            await _cartService.RemoveItemAsync(userId, productId);
            return NoContent();
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InsufficientStockException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ProductNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // api/cart/{userId}
    // Clears all of a user's items from the cart, may be useful after a "checkout"
    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        try
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
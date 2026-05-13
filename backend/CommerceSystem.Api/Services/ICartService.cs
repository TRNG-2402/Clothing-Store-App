using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Services;

public interface ICartService
{
    Task<Cart> GetCartByUserIdAsync(int userId);
    Task<Cart> AddItemAsync(int userId, AddCartItemRequest request);
    Task<Cart> UpdateItemQuantityAsync(int userId, int productId, UpdateCartItemRequest request);
    Task RemoveItemAsync(int userId, int productId);
    Task ClearCartAsync(int userId);
}
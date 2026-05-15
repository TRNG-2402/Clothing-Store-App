using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Models;
namespace CommerceSystem.Api.Services;

public interface ICartService
{
    Task<List<CartItemDTO>> GetCartItems(int id);
    Task InsertItem(int userId, int productId, int quantity);
    Task UpdateQuantity(int userId, int productId, int quantity);
    Task DeleteItem(int userId, int productId);
    //Task<Cart> GetCartByUserIdAsync(int userId);
    //Task<Cart> AddItemAsync(int userId, AddCartItemRequest request);
    //Task<Cart> UpdateItemQuantityAsync(int userId, int productId, UpdateCartItemRequest request);
    //Task RemoveItemAsync(int userId, int productId);
    Task ClearCartAsync(int userId);
}
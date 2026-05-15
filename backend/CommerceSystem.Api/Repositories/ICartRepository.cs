using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task<List<CartItemDTO>> GetCartItems(int userId);
    Task InsertItem(int userId, int productId, int quantity);
    Task UpdateQuantity(int userId, int productId, int quantity);
    Task DeleteItem(int userId, int productId);
    Task AddAsync(Cart cart);
    Task SaveChangesAsync();
}

using CommerceSystem.Api.Models;

namespace CommerceSystem.Api.Repositories;

public interface ICartRepository
{

    Task<List<CartItemDTO>> GetCartItems(int userId);
    Task InsertItem(int userId, int productId, int quantity);
    Task UpdateQuantity(int userId, int productId, int quantity);
    Task DeleteItem(int userId, int productId);
}
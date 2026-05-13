using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Services;

public interface ICartService
{
    Task<List<CartItemDTO>> GetCartItems(int id);
    Task InsertItem(int userId, int productId, int quantity);
    Task UpdateQuantity(int userId, int productId, int quantity);
    Task DeleteItem(int userId, int productId);
}
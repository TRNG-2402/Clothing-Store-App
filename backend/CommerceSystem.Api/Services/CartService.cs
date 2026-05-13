using CommerceSystem.Api.Models;
using CommerceSystem.Api.Exceptions;
using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.DTOs;


namespace CommerceSystem.Api.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<List<CartItemDTO>> GetCartItems(int id)
    {
        return await _cartRepository.GetCartItems(id);
    }

    public async Task InsertItem(int userId, int productId, int quantity)
    {
        return await _cartRepository.InsertItem(userId,productId,quantity);
    }

    public async Task UpdateQuantity(int userId, int productId, int quantity)
    {
        return await _cartRepository.UpdateQuantity(userId,productId,quantity);
    }

    public async Task DeleteItem(int userId, int productId)
    {
        return await _cartRepository.DeleteItem(userId,productId);
    }




}

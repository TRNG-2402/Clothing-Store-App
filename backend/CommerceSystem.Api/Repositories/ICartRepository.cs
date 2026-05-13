using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(int userId);
    Task AddAsync(Cart cart);
    Task SaveChangesAsync();
}

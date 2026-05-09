using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email); // For login
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<User> UpdateUserAsync(int id, UpdateUserRequest request);
}
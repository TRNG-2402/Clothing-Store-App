using CommerceSystem.Api.Models;
namespace CommerceSystem.Api.Repositories;
public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
    Task AddAsync(Category category);
    Task DeleteByIdAsync(int id);
    Task SaveChangesAsync();
}

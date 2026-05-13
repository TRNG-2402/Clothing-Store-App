using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<PagedResult<Product>> GetAllAsync(ProductQueryParams queryParams);
    Task AddAsync(Product product);
    Task<bool> SkuExistsAsync(string sku);
    Task SaveChangesAsync();
    Task DeleteByIdAsync(int id);
}
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Models;
namespace CommerceSystem.Api.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    //Task<List<Category>> GetAllCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request);
    Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryRequest request);
    Task DeleteCategoryAsync(int id);
}

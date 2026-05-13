using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Models;
namespace CommerceSystem.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // GetAllCategories
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(c => new CategoryDto
        {
            CategoryId = c.Id,
            Name = c.Name
        }).ToList();
    }
}
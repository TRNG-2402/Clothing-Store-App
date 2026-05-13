using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.Exceptions;

namespace CommerceSystem.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // GetAllCategories
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    // GetCategoryById
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception($"Category id {id} not found.");

        return category;
    }

    // GetProductsByCategory
    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category == null)
            throw new Exception($"Category id {categoryId} not found.");

        return await _categoryRepository.GetProductsByCategoryIdAsync(categoryId);
    }

    // CreateCategory
    public async Task<Category> CreateCategoryAsync(CreateCategoryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Category name required.");

        var category = new Category
        {
            Name = request.Name
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return category;
    }

    // UpdateCategory
    public async Task<Category> UpdateCategoryAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception($"Category id {id} not found.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            category.Name = request.Name;

        await _categoryRepository.SaveChangesAsync();

        return category;
    }

    // DeleteCategory
    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception($"Category id {id} not found.");

        await _categoryRepository.DeleteByIdAsync(id);
        await _categoryRepository.SaveChangesAsync();
    }
}
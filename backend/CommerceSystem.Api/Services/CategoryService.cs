using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Exceptions;

namespace CommerceSystem.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISaleService _saleService;

    public CategoryService(ICategoryRepository categoryRepository, ISaleService saleService)
    {
        _categoryRepository = categoryRepository;
        _saleService = saleService;
    }

    // GetAllCategories w/ SalesInfo (for categories page)
    public async Task<List<CategoryWithSaleDto>> GetCategoriesWithActiveSalesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var activeSales = await _saleService.GetActiveSalesAsync();

        var salesByCategory = activeSales
            .GroupBy(s => s.CategoryId)
            .ToDictionary(g => g.Key, g => g.First());

        return categories.Select(c =>
        {
            salesByCategory.TryGetValue(c.Id, out var sale);

            return new CategoryWithSaleDto
            {
                CategoryId = c.Id,
                Name = c.Name,
                HasActiveSale = sale != null,
                DiscountPercentage = sale?.DiscountPercentage
            };
        }).ToList();
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

    // GetCategoryById
    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception($"Category id {id} not found.");

        return new CategoryDto
        {
            CategoryId = category.Id,
            Name = category.Name
        };
    }

    // GetProductsByCategory
    public async Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category == null)
            throw new Exception($"Category id {categoryId} not found.");

        var products = await _categoryRepository.GetProductsByCategoryIdAsync(categoryId);

        return products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            SKU = product.SKU,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        }).ToList();
    }

    // CreateCategory
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Category name required.");

        var category = new Category
        {
            Name = request.Name
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryDto
        {
            CategoryId = category.Id,
            Name = category.Name
        };
    }

    // UpdateCategory
    public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception($"Category id {id} not found.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            category.Name = request.Name;

        await _categoryRepository.SaveChangesAsync();

        return new CategoryDto
        {
            CategoryId = category.Id,
            Name = category.Name
        };
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
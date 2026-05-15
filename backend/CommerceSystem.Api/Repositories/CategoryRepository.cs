using CommerceSystem.Api.Models;
using CommerceSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
namespace CommerceSystem.Api.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly StoreDbContext _context;

    public CategoryRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }
 
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }
 
    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
 
    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }
 
    public async Task DeleteByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
 
        if (category == null)
            return;
 
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
 
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
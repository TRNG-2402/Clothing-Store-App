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
}
using CommerceSystem.Api.Data;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CommerceSystem.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreDbContext _context;

    public ProductRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PagedResult<Product>> GetAllAsync(ProductQueryParams queryParams)
    {
        var query = _context.Products
            .AsNoTracking() // Read only
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrEmpty(queryParams.Search))
        {
            query = query.Where(p => p.Name.Contains(queryParams.Search));
        }

        if (queryParams.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == queryParams.CategoryId.Value);
        }

        if (queryParams.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= queryParams.MinPrice.Value);
        }

        if (queryParams.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= queryParams.MaxPrice.Value);
        }

        // Sort
        if (!string.IsNullOrEmpty(queryParams.SortBy))
        {
            query = queryParams.SortBy.ToLower() switch
            {
                "price" => queryParams.Descending
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price),

                "name" => queryParams.Descending
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),

                _ => query.OrderBy(p => p.Id)
            };
        }
        else
        {
            query = query.OrderBy(p => p.Id); // Default sorting by id: always ensure consistent ordering
        }


        // Count before pagination
        var totalCount = await query.CountAsync();

        // Pagination
        var items = await query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(
            totalCount / (double)queryParams.PageSize);

        return new PagedResult<Product>
        {
            Items = items,
            PageNumber = queryParams.PageNumber,
            PageSize = queryParams.PageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        //return await _context.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<bool> SkuExistsAsync(string sku)
    {
        return await _context.Products.AnyAsync(p => p.SKU == sku);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
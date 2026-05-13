using CommerceSystem.Api.Data;
using CommerceSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceSystem.Api.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly StoreDbContext _context;

    public SaleRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Category).ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var sale = await _context.Sales.FindAsync(id);

        if (sale == null)
            return;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
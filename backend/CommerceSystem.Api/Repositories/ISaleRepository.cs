using CommerceSystem.Api.Models;

namespace CommerceSystem.Api.Repositories;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(int id);
    Task<List<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);
    Task DeleteByIdAsync(int id);
    Task SaveChangesAsync();
}

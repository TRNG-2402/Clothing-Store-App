using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Services;

public interface ISaleService
{
    Task<List<Sale>> GetAllSalesAsync();
    Task<List<Sale>> GetActiveSalesAsync();
    Task<Sale> GetSaleByIdAsync(int id);
    Task<Sale> CreateSaleAsync(CreateSaleRequest request);
    Task<Sale> UpdateSaleAsync(int id, UpdateSaleRequest request);
    Task DeleteSaleAsync(int id);
}
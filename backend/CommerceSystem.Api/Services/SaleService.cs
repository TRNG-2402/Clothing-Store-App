using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.Exceptions;

namespace CommerceSystem.Api.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SaleService(ISaleRepository saleRepository, ICategoryRepository categoryRepository)
    {
        _saleRepository = saleRepository;
        _categoryRepository = categoryRepository;
    }

    // GetAllSales
    public async Task<List<Sale>> GetAllSalesAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    // GetActiveSales returns list of sales where today falls between StartDate and EndDate
    public async Task<List<Sale>> GetActiveSalesAsync()
    {
        var all = await _saleRepository.GetAllAsync();
        var now = DateTime.UtcNow;

        return all
            .Where(s => s.StartDate <= now && s.EndDate >= now).ToList();
    }

    // GetSaleById
    public async Task<Sale> GetSaleByIdAsync(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null)
            throw new Exception($"Sale id {id} not found.");
        return sale;
    }

    // CreateSale
    public async Task<Sale> CreateSaleAsync(CreateSaleRequest request)
    {
        if (request.DiscountPercentage <= 0 || request.DiscountPercentage > 100)
            throw new ArgumentException("Discount % must be between 1-100.");

        if (request.EndDate <= request.StartDate)
            throw new ArgumentException("End date must be after start date.");

        var categoryExists = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (categoryExists == null)
            throw new Exception($"Category id {request.CategoryId} not found.");

        var sale = new Sale
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DiscountPercentage = request.DiscountPercentage,
            Description = request.Description,
            CategoryId = request.CategoryId
        };
        await _saleRepository.AddAsync(sale);
        await _saleRepository.SaveChangesAsync();
        return sale;
    }

    // UpdateSale
    public async Task<Sale> UpdateSaleAsync(int id, UpdateSaleRequest request)
    {
        var sale = await _saleRepository.GetByIdAsync(id);

        if (sale == null)
            throw new Exception($"Sale id {id} not found.");

        if (request.StartDate.HasValue)
            sale.StartDate = request.StartDate.Value;

        if (request.EndDate.HasValue)
            sale.EndDate = request.EndDate.Value;

        if (sale.EndDate <= sale.StartDate)
            throw new ArgumentException("End date must be after start date.");

        if (request.DiscountPercentage.HasValue)
        {
            if (request.DiscountPercentage <= 0 || request.DiscountPercentage > 100)
                throw new ArgumentException("Discount % must be between 1-100.");

            sale.DiscountPercentage = request.DiscountPercentage.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
            sale.Description = request.Description;

        if (request.CategoryId.HasValue)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);

            if (categoryExists == null)
                throw new Exception($"Category id {request.CategoryId} not found.");

            sale.CategoryId = request.CategoryId.Value;
        }
        await _saleRepository.SaveChangesAsync();
        return sale;
    }

    // DeleteSale
    public async Task DeleteSaleAsync(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null)
            throw new Exception($"Sale id {id} not found.");

        await _saleRepository.DeleteByIdAsync(id);
        await _saleRepository.SaveChangesAsync();
    }
}
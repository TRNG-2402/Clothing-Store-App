using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Services;

public interface IProductService
{
    ProductDto MapToDto(Product product, Sale? activeSale);
    decimal CalculateFinalPrice(Product product, Sale? activeSale);
    Task<PagedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(CreateProductRequest request);
    Task DeleteProductAsync(int id);
    Task<Product> UpdateProductAsync(int id, UpdateProductRequest request);

}
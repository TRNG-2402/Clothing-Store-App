using CommerceSystem.Api.Models;
using CommerceSystem.Api.Exceptions;
using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ISaleService _saleService;

    public ProductService(IProductRepository productRepository, ISaleService saleService)
    {
        _productRepository = productRepository;
        _saleService = saleService;
    }

    // ProductDtoMapping
    private ProductDto MapToDto(Product product, Sale? activeSale)
    {
        var discount = activeSale?.DiscountPercentage ?? 0;

        var finalPrice = discount > 0
            ? product.Price - (product.Price * discount / 100)
            : product.Price;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            SKU = product.SKU,
            CategoryId = product.CategoryId,
            Description = product.Description,

            Price = product.Price,
            FinalPrice = finalPrice,
            HasActiveSale = activeSale != null,
            DiscountPercentage = activeSale?.DiscountPercentage,

            StockQuantity = product.StockQuantity
        };
    }

    // GetAllProducts
    public async Task<PagedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
    {
        var result = await _productRepository.GetAllAsync(queryParams);

        var activeSales = await _saleService.GetActiveSalesAsync();

        var salesByCategory = activeSales
            .GroupBy(s => s.CategoryId)
            .ToDictionary(g => g.Key, g => g.First());

        return new PagedResult<ProductDto>
        {
            Items = result.Items.Select(p =>
            {
                salesByCategory.TryGetValue(p.CategoryId, out var sale);
                return MapToDto(p, sale);
            }).ToList(),

            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages
        };
    }

    // GetProductById
    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new ProductNotFoundException($"Product id {id} was not found.");

        var activeSales = await _saleService.GetActiveSalesAsync();

        var salesByCategory = activeSales
            .GroupBy(s => s.CategoryId)
            .ToDictionary(g => g.Key, g => g.First());

        salesByCategory.TryGetValue(product.CategoryId, out var sale);

        return MapToDto(product, sale);
    }

    // CreateProduct
    public async Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            SKU = request.SKU,
            CategoryId = request.CategoryId,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };


        // Check for valid name
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Product name is required.");
        }

        // Negative price
        if (product.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }

        // Check for duplicate SKU
        var exists = await _productRepository.SkuExistsAsync(product.SKU);

        if (exists)
        {
            throw new DuplicateSkuException(product.SKU);
        }

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return product;
    }


    // DeleteProduct
    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new ProductNotFoundException($"Product id {id} not found.");

        await _productRepository.DeleteByIdAsync(id);
    }


    // UpdateProduct
    public async Task<Product> UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new ProductNotFoundException($"Product {id} not found.");
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            product.Name = request.Name;
        }

        if (request.Price.HasValue)
        {
            if (request.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            product.Price = request.Price.Value;
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            product.Description = request.Description;
        }

        product.CategoryId = request.CategoryId;

        await _productRepository.SaveChangesAsync();

        return product;
    }
}
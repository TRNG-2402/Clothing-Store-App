using Microsoft.EntityFrameworkCore;
using CommerceSystem.Api.Data;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Services;

namespace CommerceSystem.Api.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IWeatherService _weatherService;
    private readonly StoreDbContext _context;
    private readonly ISaleService _saleService;
    private readonly IProductService _productService;

    public RecommendationService(
        IWeatherService weatherService,
        StoreDbContext context,
        ISaleService saleService,
        IProductService productService)
    {
        _weatherService = weatherService;
        _context = context;
        _saleService = saleService;
        _productService = productService;
    }

    public async Task<RecommendationResponseDto> GetRecommendations(double lat, double lon)
    {
        // 1. Weather
        var weather = await _weatherService.GetCurrentWeather(lat, lon);

        var categoryId = MapWeatherToCategoryId(
            weather.WeatherCode,
            weather.Temperature
        );

        // 2. Active sale for that category
        var activeSales = await _saleService.GetActiveSalesAsync();

        var sale = activeSales.FirstOrDefault(s =>
            s.CategoryId == categoryId &&
            s.StartDate <= DateTime.UtcNow &&
            s.EndDate >= DateTime.UtcNow);

        // 3. Get products + INCLUDE CATEGORY (IMPORTANT FIX)
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && p.StockQuantity > 0)
            .Take(5)
            .ToListAsync();

        // 4. Map products to DTOs
        var productDtos = products
            .Select(p => _productService.MapToDto(p, sale))
            .ToList();

        // 5. Get category name from DB (NO HARDCODING)
        var categoryName = products.FirstOrDefault()?.Category?.Name ?? "Unknown";

        // 6. Return response
        return new RecommendationResponseDto
        {
            WeatherSummary =
                $"Temperature: {weather.Temperature}°C | Code: {weather.WeatherCode}",

            Category = categoryName,

            Products = productDtos
        };
    }

    // Weather -> Category mapping 
    // Mapping is hard coded:
    //1. Winter Wear
    //2. Summer Wear
    //3. Rain Gear
    //4. Shoes (or misc)
    private int MapWeatherToCategoryId(int code, double temp)
    {
        // Winter
        if (temp <= 5)
            return 1;

        // Rain
        if (code is 51 or 53 or 55 or 61 or 63 or 65 or 80 or 81 or 82)
            return 3;

        // Summer
        if (temp >= 20)
            return 2;

        // Default
        return 4;
    }
}
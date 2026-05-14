namespace CommerceSystem.Api.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } // Base price
    public decimal FinalPrice { get; set; } // Sales price (if applicable)
    public bool HasActiveSale { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public int StockQuantity { get; set; }
}
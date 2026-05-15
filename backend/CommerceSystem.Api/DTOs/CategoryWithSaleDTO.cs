namespace CommerceSystem.Api.DTOs;

public class CategoryWithSaleDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public bool HasActiveSale { get; set; }
    public decimal? DiscountPercentage { get; set; }
}
namespace CommerceSystem.Api.DTOs;

public class UpdateSaleRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
}
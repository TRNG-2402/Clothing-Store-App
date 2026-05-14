using System.ComponentModel.DataAnnotations;
namespace CommerceSystem.Api.DTOs;

public class CreateSaleRequest
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Discount % must be between 1-100.")]
    public decimal DiscountPercentage { get; set; }

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int CategoryId { get; set; }
}
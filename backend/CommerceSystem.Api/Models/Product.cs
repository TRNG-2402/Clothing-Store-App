using System.ComponentModel.DataAnnotations;
namespace CommerceSystem.Api.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public List<CartItem> CartItems { get; set; } = new();

    public Category Category { get; set; } = null!;
}
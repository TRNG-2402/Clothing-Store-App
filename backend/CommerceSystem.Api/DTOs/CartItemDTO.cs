namespace CommerceSystem.Api.DTOs;

public class CartItemDTO
{
    public string Name { get; set; } 
    public string SKU { get; set; } 
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int StockQuantity { get; set; }
    public int Id{get;set;}

}


public class AddCartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}


public class UpdateCartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
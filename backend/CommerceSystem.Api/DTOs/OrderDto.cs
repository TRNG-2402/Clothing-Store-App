namespace CommerceSystem.Api.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public string ShippingAddress { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal Total { get; set; }
}
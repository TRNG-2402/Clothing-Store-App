namespace CommerceSystem.Api.DTOs;

public class OrderSummaryDTO
{

    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; }

    public decimal Total { get; set; }

    public List<OrderItemPreviewDTO> Items { get; set; } = new();
}
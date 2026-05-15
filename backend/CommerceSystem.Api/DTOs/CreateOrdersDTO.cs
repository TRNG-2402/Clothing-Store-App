public class CreateOrderDto
{
    public string ShippingAddress { get; set; }
    public List<CreateOrderItemDto> Items { get; set; }
}


public class CreateOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal FinalPrice { get; set; }

}
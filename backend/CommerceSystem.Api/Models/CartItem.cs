using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
namespace CommerceSystem.Api.Models;

public class CartItem
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; } = null!;
    public Cart Cart { get; set; } = null!;


}
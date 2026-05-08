using System.ComponentModel.DataAnnotations;
namespace CommerceSystem.Api.Models;


public class Sale
{
    public int Id {get; set;}

    public DateTime StartDate {get; set;}

    public DateTime EndDate {get; set;}

    public int CategoryId {get; set;}

    public Decimal DiscountPercentage {get; set;}

    public string Description {get; set;} = string.Empty;

    public Category Category {get; set;} = null!;
}
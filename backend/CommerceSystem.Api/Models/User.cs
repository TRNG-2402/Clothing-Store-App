using System.ComponentModel.DataAnnotations;
namespace CommerceSystem.Api.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty; // Login with email

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "User";

    public List<Order> Orders { get; set; } = new();

    public Cart Cart { get; set; } = null!;
}
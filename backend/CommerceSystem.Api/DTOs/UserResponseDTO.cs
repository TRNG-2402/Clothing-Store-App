namespace CommerceSystem.Api.DTOs;
public class UserResponseDto
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? Name { get; set; }
}
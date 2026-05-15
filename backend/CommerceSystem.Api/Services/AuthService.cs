using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Data;

namespace CommerceSystem.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    private readonly StoreDbContext _context;

    public AuthService(IUserService userService, IConfiguration config, StoreDbContext context)
    {
        _userService = userService;
        _config = config;
        _context = context;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        // Find user
        var user = await _userService.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("Invalid credentials");

        // Verify password
        var hasher = new PasswordHasher<User>();

        var result = hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password
        );

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid credentials");

        // Generate token
        var token = GenerateJwtToken(user);

        // Return response
        return new AuthResponseDto
        {
            Token = token,
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };
    }

    private string GenerateJwtToken(User user)
    {
        string jwtKey = _config["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key missing from config.");
        string jwtIssuer = _config["Jwt:Issuer"]!;
        string jwtAudience = _config["Jwt:Audience"]!;

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserResponseDto> RegisterAsync(CreateUserRequest dto)
    {
        // Check if user already exists
        var existingUser = await _userService.GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new Exception("Email already in use");
        }

        // Hash password
        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name
        };

        user.PasswordHash = hasher.HashPassword(user, dto.Password);

        // Create user via UserService
        var createdUser = await _userService.CreateUserAsync(user);


        var cart = new Cart //assign cartId to each user
        {
            UserId = createdUser.Id
        };

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();



        // Return DTO
        return new UserResponseDto
        {
            Id = createdUser.Id,
            Email = createdUser.Email,
            Name = createdUser.Name
        };
    }
}


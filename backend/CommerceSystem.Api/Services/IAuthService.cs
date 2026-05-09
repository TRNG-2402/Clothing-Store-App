using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Services;

public interface IAuthService
{
    Task<UserResponseDto> RegisterAsync(CreateUserRequest dto);

    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}
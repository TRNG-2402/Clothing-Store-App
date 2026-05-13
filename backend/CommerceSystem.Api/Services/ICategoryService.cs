using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
}
using CommerceSystem.Api.Models;
namespace CommerceSystem.Api.Repositories;
public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
}
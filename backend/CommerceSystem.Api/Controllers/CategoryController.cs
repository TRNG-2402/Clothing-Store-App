using Microsoft.AspNetCore.Mvc;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Services;
namespace CommerceSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GetAllCategories
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories); // 200
    }
}
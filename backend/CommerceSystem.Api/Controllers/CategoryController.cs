using Microsoft.AspNetCore.Mvc;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Exceptions;

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

    // GetAllCategories w/ SalesInfo
    [AllowAnonymous]
    [HttpGet("with-sales")]
    public async Task<IActionResult> GetCategoriesWithSales()
    {
        var result = await _categoryService.GetCategoriesWithActiveSalesAsync();
        return Ok(result); // 200
    }

    // GetAllCategories
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories); // 200
    }

    // api/category/{id}
    // viewing a category does not require login
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    // api/category/{id}/products
    // browsing products in a category does not require login
    [AllowAnonymous]
    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetProductsByCategory(int id)
    {
        try
        {
            var products = await _categoryService.GetProductsByCategoryAsync(id);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // api/category
    // only admins should be creating categories
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        try
        {
            var category = await _categoryService.CreateCategoryAsync(request);
            //return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            return CreatedAtAction(
                nameof(GetById),
                new { id = category.CategoryId },
                category
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // api/category/{id}
    // admin only
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request)
    {
        try
        {
            var category = await _categoryService.UpdateCategoryAsync(id, request);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // api/category/{id}
    // admin only
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
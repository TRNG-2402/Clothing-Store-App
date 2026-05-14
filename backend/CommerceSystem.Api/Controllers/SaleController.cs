using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.Exceptions;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    // api/sale
    // anyone can see all sales
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllSalesAsync();
        return Ok(sales);
    }

    // api/sale/active
    // get all active sales
    [AllowAnonymous]
    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var sales = await _saleService.GetActiveSalesAsync();
        return Ok(sales);
    }

    // api/sale/{id}
    // Get sale by id
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // api/sale
    // Only admins can create sales
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
    {
        try
        {
            var sale = await _saleService.CreateSaleAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // api/sale/{id}
    // only admins can update sales
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleRequest request)
    {
        try
        {
            var sale = await _saleService.UpdateSaleAsync(id, request);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // api/sale/{id}
    // only admins can delete sales
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
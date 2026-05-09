using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.Exceptions;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /* Removed User Creation endpoint; this is handled by Auth
    // CreateUser
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Create(CreateUserRequest request)
    {
        try
        {
            var user = await _userService.CreateUserAsync(request);

            var dto = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };


            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                dto
            ); // 201
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); // 400
        }
    }
    */
    // GetUser -> only get the loggedin user
    [HttpGet("me")]
    public async Task<ActionResult<UserResponseDto>> GetMe()
    {
        try
        {
            // Get id from token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var user = await _userService.GetByIdAsync(userId);

            var dto = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };

            return Ok(dto);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message); // 404
        }
    }

    // GetUserOrders
    [HttpGet("me/orders")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        try
        {
            // Get id from token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            var orders = await _userService.GetOrdersByUserIdAsync(userId);

            var result = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Total = o.Total,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });

            return Ok(result);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message); // 404
        }
    }

    // UpdateUser (patch)
    [HttpPatch("{id}")]
    public async Task<ActionResult<UserResponseDto>> Update(int id, UpdateUserRequest request)
    {
        try
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request);

            var dto = new UserResponseDto
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                Name = updatedUser.Name
            };

            return Ok(dto); // 200
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message); // 404
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); // 400
        }
    }
}
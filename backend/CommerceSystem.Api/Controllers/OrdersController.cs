using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Exceptions;

using CommerceSystem.Api.Data;
using System.Security.Claims;

using System.Net;
using System.Runtime.CompilerServices;


namespace CommerceSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly StoreDbContext _context;


    public OrdersController(IOrderService orderService, StoreDbContext context)
    {
        _orderService = orderService;
        _context = context;
    }

    // CreateOrder
    [HttpPost]
    public async Task<ActionResult<Order>> Create(CreateOrderRequest request)
    {
        try
        {
            var order = await _orderService.CreateOrderAsync(request);

            var dto = new OrderDto
            {
                Id = order.Id,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.Id },
                dto
            ); // 201

            /*
            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    userId = order.UserId,
                    orderId = order.Id
                },
            dto
            ); // 201
            */
        }
        catch (ProductNotFoundException ex)
        {
            return NotFound(ex.Message); // 404
        }
        catch (InsufficientStockException ex)
        {
            return BadRequest(ex.Message); // 400
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); // 400
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message); // 404  
        }
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized();

        if (!int.TryParse(userIdString, out int userId))
            return Unauthorized(); // or BadRequest("Invalid user id");

        TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, eastern);

        var order = new Order
        {
            ShippingAddress = dto.ShippingAddress,
            CreatedAt = easternTime,
            UserId = userId,
        };

        decimal total = 0;

        foreach (var item in dto.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);

            var orderItem = new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };

            total += orderItem.UnitPrice * orderItem.Quantity;

          order.Items.Add(orderItem);
        }

        order.Total = total;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(order.Id);
    }



    // GetOrderById
    [HttpGet("{id}")]
    //[HttpGet("/api/users/{userId}/orders/{orderId}")]
    public async Task<ActionResult<Order>> GetById(int id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            var dto = new OrderDto
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                ShippingAddress = order.ShippingAddress,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return Ok(dto); // 200
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(ex.Message); // 404
        }


    }

    // PatchOrderById (OrderStatus)
    [HttpPatch("{id}")]
    public async Task<ActionResult<Order>> Update(int id, UpdateOrderRequest request)
    {
        try
        {
            var order = await _orderService.UpdateOrderAsync(id, request);

            var dto = new OrderDto
            {
                Id = order.Id,
                Total = order.Total,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return Ok(dto); // 200
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(ex.Message); // 404
        }
    }


[HttpGet("my-orders")]
[Authorize]
public async Task<ActionResult<List<OrderSummaryDTO>>> GetMyOrders()
{
    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    var result = await _orderService.GetMyOrdersAsync(userId);

    return Ok(result);
}
}
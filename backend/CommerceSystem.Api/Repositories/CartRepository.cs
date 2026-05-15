using CommerceSystem.Api.Data;
using CommerceSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Repositories;

public class CartRepository : ICartRepository
{
    private readonly StoreDbContext _context;

    public CartRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<List<CartItemDTO>> GetCartItems(int userId)
    {
        var items = await _context.CartItems
        .Where(ci => ci.Cart.UserId == userId)
        .Select(ci => new CartItemDTO
        {
            Id = ci.Product.Id,
            Name = ci.Product.Name,
            SKU = ci.Product.SKU,
            Price = ci.Product.Price,
            Quantity = ci.Quantity,
            StockQuantity = ci.Product.StockQuantity
        })
            .ToListAsync();

        return items;
        //auto handles when empty
    }


    //insertItem
    public async Task InsertItem(int userId, int productId, int quantity)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>()
            };

            _context.Carts.Add(cart);
        }

        var existingItem = cart.CartItems
            .FirstOrDefault(ci => ci.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity
            });
        }

        await _context.SaveChangesAsync();
    }


    //updateQuantity
    public async Task UpdateQuantity(int userId, int productId, int quantity)
    {
        var cart = await _context.Carts
    .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
            return;

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci =>
                ci.CartId == cart.Id &&
                ci.ProductId == productId);

        if (cartItem == null)
        {
            System.Console.WriteLine("cartItem not found");
            System.Console.WriteLine(userId);
            System.Console.WriteLine(cart.Id);
            System.Console.WriteLine(productId);


            return;
        }
            


        if (quantity <= 0)
        {
            _context.CartItems.Remove(cartItem);
        }
        else
        {
            cartItem.Quantity = quantity;
        }

        await _context.SaveChangesAsync();
    }

    //deleteItem
    public async Task DeleteItem(int userId, int productId)
    {
        var cartItem = await _context.CartItems
            .Where(ci => ci.Cart.UserId == userId && ci.ProductId == productId)
            .FirstOrDefaultAsync();

        if (cartItem == null)
            return; // or throw

        _context.CartItems.Remove(cartItem);

        await _context.SaveChangesAsync();
    }




    public async Task AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

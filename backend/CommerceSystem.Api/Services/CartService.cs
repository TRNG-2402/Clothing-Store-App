using CommerceSystem.Api.Models;
using CommerceSystem.Api.Exceptions;
using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }


     public async Task<List<CartItemDTO>> GetCartItems(int id)
    {
        return await _cartRepository.GetCartItems(id);
    }

    public async Task InsertItem(int userId, int productId, int quantity)
    {
         await _cartRepository.InsertItem(userId,productId,quantity);
    }

    public async Task UpdateQuantity(int userId, int productId, int quantity)
    {
         await _cartRepository.UpdateQuantity(userId,productId,quantity);
    }

    public async Task DeleteItem(int userId, int productId)
    {
         await _cartRepository.DeleteItem(userId,productId);
    }




    /* // GetCartByUserId
    public async Task<Cart> GetCartByUserIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new UserNotFoundException($"User id {userId} was not found.");

        var cart = await _cartRepository.GetByUserIdAsync(userId);

        if (cart == null)
            throw new Exception($"Cart for user id {userId} was not found.");

        return cart;
    }

    // AddItem
    public async Task<Cart> AddItemAsync(int userId, AddCartItemRequest request)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId);

        if (cart == null)
            throw new Exception($"Cart for user id {userId} was not found.");

        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product == null)
            throw new ProductNotFoundException($"Product id {request.ProductId} was not found.");

        if (request.Quantity < 1)
            throw new ArgumentException("Quantity must be at least 1.");

        if (product.StockQuantity < request.Quantity)
            throw new InsufficientStockException($"Not enough stock for {product.Name}.");

        // If the item already exists in the cart, increase quantity
        var existingItem = cart.CartItems
            .FirstOrDefault(ci => ci.ProductId == request.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
        }

        await _cartRepository.SaveChangesAsync();

        return cart;
    }

    // UpdateItemQuantity
    public async Task<Cart> UpdateItemQuantityAsync(int userId, int productId, UpdateCartItemRequest request)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId);

        if (cart == null)
            throw new Exception($"Cart for user {userId} not found.");

        var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (item == null)
            throw new Exception($"Product {productId} not found in cart.");

        if (request.Quantity < 1)
            throw new ArgumentException("Quantity must be at least 1.");

        var product = await _productRepository.GetByIdAsync(productId);

        if (product != null && product.StockQuantity < request.Quantity)
            throw new InsufficientStockException($"Not enough stock to add {product.Name} to cart.");

        item.Quantity = request.Quantity;

        await _cartRepository.SaveChangesAsync();

        return cart;
    }

    // RemoveItem
    public async Task RemoveItemAsync(int userId, int productId)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId);

        if (cart == null)
            throw new Exception($"Cart for user id {userId} was not found.");

        var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        if (item == null)
            throw new Exception($"Product id {productId} was not found in cart.");

        cart.CartItems.Remove(item);

        await _cartRepository.SaveChangesAsync();
    }
    */

    // ClearCart, call after a successful "checkout"
    public async Task ClearCartAsync(int userId)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId);

        if (cart == null)
            throw new Exception($"Cart for user id {userId} was not found.");

        cart.CartItems.Clear();

        await _cartRepository.SaveChangesAsync();
    }
    
}
 
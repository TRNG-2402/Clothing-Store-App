using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using CommerceSystem.Api.Repositories;
using CommerceSystem.Api.Data;
using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;
using CommerceSystem.Api.Exceptions;

namespace CommerceSystem.Tests.Repositories;

public class ProductRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_WithPagination_ReturnsCorrectPage()
    {
        var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new StoreDbContext(options);

        context.Products.AddRange(
            new Product { Id = 1, Name = "A", Price = 10, CategoryId = 1 },
            new Product { Id = 2, Name = "B", Price = 20, CategoryId = 1 },
            new Product { Id = 3, Name = "C", Price = 30, CategoryId = 1 }
        );

        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);

        var queryParams = new ProductQueryParams
        {
            PageNumber = 2,
            PageSize = 1
        };

        var result = await repo.GetAllAsync(queryParams);

        Assert.Single(result.Items);
        Assert.Equal(2, result.Items.First().Id);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(3, result.TotalPages);
    }

    [Fact]
    public async Task GetAllAsync_WithCategoryFilter_ReturnsOnlyMatching()
    {
        var options = new DbContextOptionsBuilder<StoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new StoreDbContext(options);

        context.Products.AddRange(
            new Product { Id = 1, Name = "A", Price = 10, CategoryId = 1 },
            new Product { Id = 2, Name = "B", Price = 20, CategoryId = 2 }
        );

        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);

        var queryParams = new ProductQueryParams
        {
            CategoryId = 1,
            PageNumber = 1,
            PageSize = 10
        };

        var result = await repo.GetAllAsync(queryParams);

        Assert.Single(result.Items);
        Assert.All(result.Items, p => Assert.Equal(1, p.CategoryId));
    }

}
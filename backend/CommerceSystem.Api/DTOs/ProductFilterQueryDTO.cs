namespace CommerceSystem.Api.DTOs;

public class ProductQueryParams
{
    // page limitation
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    private int _pageNumber = 1;

    // Search / Filtering
    public int? CategoryId { get; set; }
    public string? SortBy { get; set; } // "name", "price", "id"
    public bool Descending { get; set; }
    

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string? Search { get; set; }

    // Pagination
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize
            ? MaxPageSize
            : value;
    }
}
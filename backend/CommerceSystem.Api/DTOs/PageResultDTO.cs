namespace CommerceSystem.Api.DTOs;
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new(); // The list of products returned

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }
}
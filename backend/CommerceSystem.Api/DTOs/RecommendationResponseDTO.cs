namespace CommerceSystem.Api.DTOs;

public class RecommendationResponseDto
{
    public string WeatherSummary { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<ProductDto> Products { get; set; } = new();
}
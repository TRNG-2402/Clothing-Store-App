using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Services;

public interface IRecommendationService
{
    Task<RecommendationResponseDto> GetRecommendations(double lat, double lon);
}
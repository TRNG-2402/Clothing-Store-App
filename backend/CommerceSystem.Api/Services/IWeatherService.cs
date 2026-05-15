using CommerceSystem.Api.DTOs;
namespace CommerceSystem.Api.Services;

public interface IWeatherService
{
    Task<WeatherDto> GetCurrentWeather(double lat, double lon);
}
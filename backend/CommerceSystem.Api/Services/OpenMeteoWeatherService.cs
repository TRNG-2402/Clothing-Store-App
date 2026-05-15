using CommerceSystem.Api.Models;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Services;

using System.Net.Http.Json;

public class OpenMeteoWeatherService : IWeatherService
{
    private readonly HttpClient _http;

    public OpenMeteoWeatherService(HttpClient http)
    {
        _http = http;
    }

    public async Task<WeatherDto> GetCurrentWeather(double lat, double lon)
    {
        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

        var response = await _http.GetFromJsonAsync<OpenMeteoResponse>(url);

        return new WeatherDto
        {
            Temperature = response?.current_weather.temperature ?? 0,
            WeatherCode = response?.current_weather.weathercode ?? 0
        };
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CommerceSystem.Api.Services;
using CommerceSystem.Api.DTOs;

namespace CommerceSystem.Api.Controllers;

[ApiController]
[Route("api/recommendations")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _service;

    public RecommendationController(IRecommendationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(double lat, double lon)
    {
        var result = await _service.GetRecommendations(lat, lon);
        return Ok(result);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contracts.Repositories;

namespace RentCar.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController(IHealthRepository repository) : ControllerBase
    {
        private readonly IHealthRepository _repository = repository;

        [HttpGet("live")]
        public IActionResult Live()
        {
            return Ok(new { status = "Alive", timestamp = DateTime.UtcNow });
        }

        [Authorize]
        [HttpGet("health")]
        public async Task<IActionResult> HealthCheckAsync()
        {
            return Ok(new
            {
                status = "Secure",
                user = User.Identity?.Name ?? "anonymous",
                db = await _repository.IsHealthyDataBaseConnection() ? "Healthy" : "Unhealthy",
                time = DateTime.UtcNow
            });
        }
    }
}

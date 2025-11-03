using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentCar.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController() : ControllerBase
    {
        [HttpGet("live")]
        public IActionResult Live()
        {
            return Ok(new { status = "Alive", timestamp = DateTime.UtcNow });
        }

        [Authorize]
        [HttpGet("health")]
        public IActionResult HealthCheckAsync()
        {
            return Ok(new
            {
                status = "Secure",
                user = User.Identity?.Name ?? "anonymous",
                //db = await _repository.IsHealthy() ? "Healthy" : "Unhealthy",
                time = DateTime.UtcNow
            });
        }
    }
}

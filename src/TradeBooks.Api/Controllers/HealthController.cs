using Microsoft.AspNetCore.Mvc;

namespace TradeBooks.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "ok", utc = DateTime.UtcNow });
}

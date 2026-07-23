using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TradeBooks.Api.Controllers;

[ApiController]
[Route("api/health")]
[Authorize(Policy = "AdminOnly")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "ok", utc = DateTime.UtcNow });
}

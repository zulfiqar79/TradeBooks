using Microsoft.AspNetCore.Mvc;
using TradeBooks.AuditService.Models;
using TradeBooks.AuditService.Services;

namespace TradeBooks.AuditService.Controllers;

[ApiController]
[Route("api/audit-records")]
public class AuditRecordsController(AuditStorage auditStorage) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public IActionResult Create([FromBody] AuditRecord record)
    {
        auditStorage.Insert(record);
        return Accepted(new { message = "Record accepted" });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetLatest([FromQuery] int take = 50)
    {
        take = Math.Clamp(take, 1, 200);
        var records = auditStorage.GetLatest(take);
        return Ok(records);
    }
}

namespace TradeBooks.Domain.Entities;

public class SystemAuditLog
{
    public long Id { get; set; }
    public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
    public int? UserId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
    public string Result { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

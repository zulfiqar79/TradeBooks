namespace TradeBooks.Domain.Entities;

public class BusinessTransactionLog
{
    public long Id { get; set; }
    public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string? PreviousState { get; set; }
    public string? NewState { get; set; }
    public string? Description { get; set; }
}

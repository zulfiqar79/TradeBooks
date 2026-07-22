namespace TradeBooks.Application.Interfaces.Logging;

public interface IExceptionLogStore
{
    Task StoreAsync(ExceptionLogEntry entry, CancellationToken cancellationToken = default);
}

public sealed record ExceptionLogEntry(
    DateTime OccurredAtUtc,
    string Level,
    string Message,
    string? StackTrace,
    string Path,
    string? UserId);

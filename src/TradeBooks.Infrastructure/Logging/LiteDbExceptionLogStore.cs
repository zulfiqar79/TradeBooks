using LiteDB;
using TradeBooks.Application.Interfaces.Logging;

namespace TradeBooks.Infrastructure.Logging;

public class LiteDbExceptionLogStore : IExceptionLogStore
{
    private readonly string _databasePath;

    public LiteDbExceptionLogStore(string databasePath)
    {
        _databasePath = databasePath;
    }

    public Task StoreAsync(ExceptionLogEntry entry, CancellationToken cancellationToken = default)
    {
        using var database = new LiteDatabase(_databasePath);
        var collection = database.GetCollection<ExceptionLogEntry>("exception_logs");
        collection.Insert(entry);
        return Task.CompletedTask;
    }
}

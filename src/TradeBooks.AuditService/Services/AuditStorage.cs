using LiteDB;
using TradeBooks.AuditService.Models;

namespace TradeBooks.AuditService.Services;

public class AuditStorage
{
    private readonly string _dbPath;

    public AuditStorage(IConfiguration configuration)
    {
        _dbPath = configuration["LiteDb:AuditPath"] ?? "audit-service.db";
    }

    public void Insert(AuditRecord record)
    {
        using var db = new LiteDatabase(_dbPath);
        var col = db.GetCollection<AuditRecord>("audit_records");
        col.Insert(record);
    }

    public IReadOnlyCollection<AuditRecord> GetLatest(int take)
    {
        using var db = new LiteDatabase(_dbPath);
        var col = db.GetCollection<AuditRecord>("audit_records");
        return col.Query().OrderByDescending(x => x.OccurredAtUtc).Limit(take).ToList();
    }
}

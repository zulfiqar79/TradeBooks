using Microsoft.EntityFrameworkCore;
using TradeBooks.Application.Interfaces.Repositories;
using TradeBooks.Domain.Entities;
using TradeBooks.Infrastructure.Persistence;

namespace TradeBooks.Infrastructure.Repositories;

public class ExchangeRepository(TradeBooksDbContext dbContext) : IExchangeRepository
{
    public async Task<Exchange> AddAsync(Exchange exchange, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Exchanges.AddAsync(exchange, cancellationToken);
        return entry.Entity;
    }

    public Task<Exchange?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        dbContext.Exchanges.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}

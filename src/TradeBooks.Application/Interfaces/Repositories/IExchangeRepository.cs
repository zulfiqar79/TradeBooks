using TradeBooks.Domain.Entities;

namespace TradeBooks.Application.Interfaces.Repositories;

public interface IExchangeRepository
{
    Task<Exchange?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Exchange> AddAsync(Exchange exchange, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

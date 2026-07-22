using TradeBooks.Domain.Entities;

namespace TradeBooks.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default);
    Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Book>> SearchAsync(string? query, int skip, int take, CancellationToken cancellationToken = default);
    Task<int> CountAsync(string? query, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

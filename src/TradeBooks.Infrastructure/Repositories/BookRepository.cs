using Microsoft.EntityFrameworkCore;
using TradeBooks.Application.Interfaces.Repositories;
using TradeBooks.Domain.Entities;
using TradeBooks.Infrastructure.Persistence;

namespace TradeBooks.Infrastructure.Repositories;

public class BookRepository(TradeBooksDbContext dbContext) : IBookRepository
{
    public async Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Books.AddAsync(book, cancellationToken);
        return entry.Entity;
    }

    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        dbContext.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<Book>> SearchAsync(string? query, int skip, int take, CancellationToken cancellationToken = default)
    {
        var books = dbContext.Books.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(query))
        {
            books = books.Where(x => x.Title.Contains(query) || x.Author.Contains(query));
        }

        return await books
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(string? query, CancellationToken cancellationToken = default)
    {
        var books = dbContext.Books.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(query))
        {
            books = books.Where(x => x.Title.Contains(query) || x.Author.Contains(query));
        }

        return await books.CountAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}

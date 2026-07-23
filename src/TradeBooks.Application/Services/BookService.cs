using TradeBooks.Application.Common;
using TradeBooks.Application.DTOs;
using TradeBooks.Application.Interfaces.Repositories;
using TradeBooks.Application.Interfaces.Services;
using TradeBooks.Domain.Entities;

namespace TradeBooks.Application.Services;

public class BookService(IBookRepository bookRepository, IUserRepository userRepository) : IBookService
{
    public async Task<BookReadDto> CreateAsync(BookCreateDto dto, string authenticatedAuth0UserId, CancellationToken cancellationToken = default)
    {
        var owner = await userRepository.GetByAuth0UserIdAsync(authenticatedAuth0UserId, cancellationToken);
        if (owner is null)
        {
            throw new UnauthorizedAccessException("Authenticated user cannot publish books.");
        }

        var book = new Book
        {
            OwnerUserId = owner.Id,
            Title = dto.Title,
            Author = dto.Author,
            Isbn = dto.Isbn,
            Genre = dto.Genre,
            Location = dto.Location,
            Description = dto.Description
        };

        var created = await bookRepository.AddAsync(book, cancellationToken);
        await bookRepository.SaveChangesAsync(cancellationToken);

        return Map(created);
    }

    public async Task<BookReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);
        return book is null ? null : Map(book);
    }

    public async Task<PagedResult<BookReadDto>> SearchAsync(string? query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var skip = (pageNumber - 1) * pageSize;
        var books = await bookRepository.SearchAsync(query, skip, pageSize, cancellationToken);
        var totalCount = await bookRepository.CountAsync(query, cancellationToken);

        var items = books.Select(Map).ToArray();
        return new PagedResult<BookReadDto>(items, pageNumber, pageSize, totalCount);
    }

    private static BookReadDto Map(Book book) =>
        new(
            book.Id,
            book.OwnerUserId,
            book.Title,
            book.Author,
            book.Isbn,
            book.Genre,
            book.Location,
            book.Description,
            book.Status,
            book.IsActive);
}

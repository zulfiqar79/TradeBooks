using TradeBooks.Application.Common;
using TradeBooks.Application.DTOs;

namespace TradeBooks.Application.Interfaces.Services;

public interface IBookService
{
    Task<BookReadDto> CreateAsync(BookCreateDto dto, CancellationToken cancellationToken = default);
    Task<PagedResult<BookReadDto>> SearchAsync(string? query, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}

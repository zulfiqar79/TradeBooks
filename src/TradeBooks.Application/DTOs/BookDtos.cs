using System.ComponentModel.DataAnnotations;
using TradeBooks.Domain.Enums;

namespace TradeBooks.Application.DTOs;

public sealed record BookCreateDto(
    [property: Required, StringLength(200)] string Title,
    [property: Required, StringLength(150)] string Author,
    [property: StringLength(40)] string? Isbn,
    [property: StringLength(80)] string? Genre,
    [property: StringLength(120)] string? Location,
    [property: StringLength(1000)] string? Description);

public sealed record BookReadDto(
    int Id,
    int OwnerUserId,
    string Title,
    string Author,
    string? Isbn,
    string? Genre,
    string? Location,
    string? Description,
    BookStatus Status,
    bool IsActive);

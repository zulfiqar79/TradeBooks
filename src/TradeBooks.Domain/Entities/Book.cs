using TradeBooks.Domain.Enums;

namespace TradeBooks.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public int OwnerUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? Isbn { get; set; }
    public string? Genre { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; private set; } = true;
    public BookStatus Status { get; private set; } = BookStatus.Disponible;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public User? OwnerUser { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Exchange> RequestedInExchanges { get; set; } = new List<Exchange>();
    public ICollection<Exchange> OfferedInExchanges { get; set; } = new List<Exchange>();

    public bool TryChangeStatus(BookStatus newStatus)
    {
        var allowed = Status switch
        {
            BookStatus.Disponible => newStatus is BookStatus.Reservado or BookStatus.IntercambioPendiente,
            BookStatus.Reservado => newStatus is BookStatus.Disponible or BookStatus.IntercambioPendiente,
            BookStatus.IntercambioPendiente => newStatus is BookStatus.Disponible or BookStatus.Intercambiado,
            BookStatus.Intercambiado => false,
            _ => false
        };

        if (!allowed)
        {
            return false;
        }

        Status = newStatus;
        return true;
    }

    public bool Deactivate()
    {
        if (Status is BookStatus.Reservado or BookStatus.IntercambioPendiente)
        {
            return false;
        }

        IsActive = false;
        return true;
    }
}

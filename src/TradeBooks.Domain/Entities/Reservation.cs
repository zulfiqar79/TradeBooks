using TradeBooks.Domain.Enums;

namespace TradeBooks.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime ReservedAtUtc { get; private set; } = DateTime.UtcNow;
    public ReservationStatus Status { get; private set; } = ReservationStatus.Vigente;
    public Book? Book { get; set; }
    public User? User { get; set; }

    public bool Cancel()
    {
        if (Status != ReservationStatus.Vigente)
        {
            return false;
        }

        Status = ReservationStatus.Cancelada;
        return true;
    }
}

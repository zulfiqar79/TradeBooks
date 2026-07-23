using TradeBooks.Domain.Enums;

namespace TradeBooks.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Auth0UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public SystemRole Role { get; private set; } = SystemRole.Lector;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Exchange> RequestedExchanges { get; set; } = new List<Exchange>();
    public ICollection<Exchange> ReceivedExchanges { get; set; } = new List<Exchange>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public void AssignRole(SystemRole role) => Role = role;

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}

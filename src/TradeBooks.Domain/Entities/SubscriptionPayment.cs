using TradeBooks.Domain.Enums;

namespace TradeBooks.Domain.Entities;

public class SubscriptionPayment
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? ExternalReference { get; set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public PaymentStatus Status { get; private set; } = PaymentStatus.Pendiente;

    public bool SetStatus(PaymentStatus newStatus)
    {
        var allowed = Status switch
        {
            PaymentStatus.Pendiente => newStatus is PaymentStatus.Aprobado or PaymentStatus.Rechazado or PaymentStatus.Anulado,
            _ => false
        };

        if (!allowed)
        {
            return false;
        }

        Status = newStatus;
        return true;
    }
}

using TradeBooks.Domain.Enums;

namespace TradeBooks.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PlanName { get; set; } = "Basic";
    public DateTime StartDateUtc { get; private set; }
    public DateTime EndDateUtc { get; private set; }
    public SubscriptionStatus Status { get; private set; } = SubscriptionStatus.Pendiente;

    public bool Activate(DateTime startDateUtc, DateTime endDateUtc)
    {
        if (Status is not (SubscriptionStatus.Pendiente or SubscriptionStatus.Vencida))
        {
            return false;
        }

        StartDateUtc = startDateUtc;
        EndDateUtc = endDateUtc;
        Status = SubscriptionStatus.Activa;
        return true;
    }

    public bool MarkExpired()
    {
        if (Status != SubscriptionStatus.Activa)
        {
            return false;
        }

        Status = SubscriptionStatus.Vencida;
        return true;
    }

    public bool Cancel()
    {
        if (Status is SubscriptionStatus.Cancelada or SubscriptionStatus.Rechazada)
        {
            return false;
        }

        Status = SubscriptionStatus.Cancelada;
        return true;
    }
}

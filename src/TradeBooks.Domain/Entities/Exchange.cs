using TradeBooks.Domain.Enums;

namespace TradeBooks.Domain.Entities;

public class Exchange
{
    public int Id { get; set; }
    public int RequesterUserId { get; set; }
    public int ReceiverUserId { get; set; }
    public int RequestedBookId { get; set; }
    public int OfferedBookId { get; set; }
    public DateTime RequestedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? DecidedAtUtc { get; private set; }
    public DateTime? FinishedAtUtc { get; private set; }
    public ExchangeStatus Status { get; private set; } = ExchangeStatus.Solicitado;
    public User? RequesterUser { get; set; }
    public User? ReceiverUser { get; set; }
    public Book? RequestedBook { get; set; }
    public Book? OfferedBook { get; set; }

    public bool Approve()
    {
        if (Status != ExchangeStatus.Solicitado)
        {
            return false;
        }

        Status = ExchangeStatus.Asignado;
        DecidedAtUtc = DateTime.UtcNow;
        return true;
    }

    public bool Reject()
    {
        if (Status != ExchangeStatus.Solicitado)
        {
            return false;
        }

        Status = ExchangeStatus.Rechazado;
        DecidedAtUtc = DateTime.UtcNow;
        return true;
    }

    public bool Cancel()
    {
        if (Status is not (ExchangeStatus.Solicitado or ExchangeStatus.Asignado))
        {
            return false;
        }

        Status = ExchangeStatus.Cancelado;
        DecidedAtUtc = DateTime.UtcNow;
        return true;
    }

    public bool Complete()
    {
        if (Status != ExchangeStatus.Asignado)
        {
            return false;
        }

        Status = ExchangeStatus.Finalizado;
        FinishedAtUtc = DateTime.UtcNow;
        return true;
    }
}

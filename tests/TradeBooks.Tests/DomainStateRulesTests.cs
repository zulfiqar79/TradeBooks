using TradeBooks.Domain.Entities;
using TradeBooks.Domain.Enums;

namespace TradeBooks.Tests;

public class DomainStateRulesTests
{
    [Fact]
    public void Book_Cannot_Move_From_Intercambiado_To_Disponible()
    {
        var book = new Book();

        book.TryChangeStatus(BookStatus.IntercambioPendiente);
        book.TryChangeStatus(BookStatus.Intercambiado);

        var changed = book.TryChangeStatus(BookStatus.Disponible);

        Assert.False(changed);
        Assert.Equal(BookStatus.Intercambiado, book.Status);
    }

    [Fact]
    public void Exchange_Can_Be_Approved_Only_From_Solicitado()
    {
        var exchange = new Exchange();

        var approved = exchange.Approve();
        var approvedAgain = exchange.Approve();

        Assert.True(approved);
        Assert.False(approvedAgain);
        Assert.Equal(ExchangeStatus.Asignado, exchange.Status);
    }
}

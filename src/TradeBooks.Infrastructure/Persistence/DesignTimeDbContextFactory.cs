using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TradeBooks.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TradeBooksDbContext>
{
    public TradeBooksDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TradeBooksDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TradeBooksDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new TradeBooksDbContext(optionsBuilder.Options);
    }
}

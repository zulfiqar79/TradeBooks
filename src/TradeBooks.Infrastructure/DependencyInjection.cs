using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeBooks.Application.Interfaces.Logging;
using TradeBooks.Application.Interfaces.Repositories;
using TradeBooks.Infrastructure.Logging;
using TradeBooks.Infrastructure.Persistence;
using TradeBooks.Infrastructure.Repositories;

namespace TradeBooks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TradeBooksDb")
            ?? throw new InvalidOperationException("Connection string 'TradeBooksDb' was not found.");

        services.AddDbContext<TradeBooksDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExchangeRepository, ExchangeRepository>();

        var liteDbPath = configuration["LiteDb:ExceptionLogsPath"] ?? "tradebooks-exceptions.db";
        services.AddSingleton<IExceptionLogStore>(_ => new LiteDbExceptionLogStore(liteDbPath));

        return services;
    }
}

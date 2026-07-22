using Microsoft.EntityFrameworkCore;
using TradeBooks.Application.Interfaces.Repositories;
using TradeBooks.Domain.Entities;
using TradeBooks.Infrastructure.Persistence;

namespace TradeBooks.Infrastructure.Repositories;

public class UserRepository(TradeBooksDbContext dbContext) : IUserRepository
{
    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Users.AddAsync(user, cancellationToken);
        return entry.Entity;
    }

    public Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}

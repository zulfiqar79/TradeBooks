using Microsoft.EntityFrameworkCore;
using TradeBooks.Domain.Entities;

namespace TradeBooks.Infrastructure.Persistence;

public class TradeBooksDbContext(DbContextOptions<TradeBooksDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Exchange> Exchanges => Set<Exchange>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<SubscriptionPayment> SubscriptionPayments => Set<SubscriptionPayment>();
    public DbSet<BusinessTransactionLog> BusinessTransactionLogs => Set<BusinessTransactionLog>();
    public DbSet<SystemAuditLog> SystemAuditLogs => Set<SystemAuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", tableBuilder => tableBuilder.IsTemporal());
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.FullName).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Auth0UserId).HasMaxLength(120);
            entity.Property(x => x.Role).HasConversion<string>().HasMaxLength(30);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books", tableBuilder => tableBuilder.IsTemporal());
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Author).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Isbn).HasMaxLength(40);
            entity.Property(x => x.Genre).HasMaxLength(80);
            entity.Property(x => x.Location).HasMaxLength(120);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(40);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservations");
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
        });

        modelBuilder.Entity<Exchange>(entity =>
        {
            entity.ToTable("Exchanges", tableBuilder => tableBuilder.IsTemporal());
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.ToTable("Subscriptions", tableBuilder => tableBuilder.IsTemporal());
            entity.Property(x => x.PlanName).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
        });

        modelBuilder.Entity<SubscriptionPayment>(entity =>
        {
            entity.ToTable("SubscriptionPayments", tableBuilder => tableBuilder.IsTemporal());
            entity.Property(x => x.PaymentMethod).HasMaxLength(60).IsRequired();
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            entity.Property(x => x.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<BusinessTransactionLog>(entity =>
        {
            entity.ToTable("BusinessTransactionLogs");
            entity.Property(x => x.Action).HasMaxLength(120).IsRequired();
            entity.Property(x => x.EntityType).HasMaxLength(120).IsRequired();
            entity.Property(x => x.EntityId).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<SystemAuditLog>(entity =>
        {
            entity.ToTable("SystemAuditLogs");
            entity.Property(x => x.EventType).HasMaxLength(120).IsRequired();
            entity.Property(x => x.IpAddress).HasMaxLength(64);
            entity.Property(x => x.Result).HasMaxLength(60).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(1000).IsRequired();
        });
    }
}

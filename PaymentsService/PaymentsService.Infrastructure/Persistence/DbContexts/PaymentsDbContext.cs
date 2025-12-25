using Microsoft.EntityFrameworkCore;
using PaymentsService.Domain.Accounts;
using PaymentsService.Application.Inbox;
using PaymentsService.Application.Outbox;
using PaymentsService.Infrastructure.Persistence.DbContexts.EntityConfigurations;

namespace PaymentsService.Infrastructure.Persistence.DbContexts;

public class PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : DbContext(options)
{
    public DbSet<BankAccount> Accounts => Set<BankAccount>();
    public DbSet<InboxMessage> Inbox => Set<InboxMessage>();
    public DbSet<OutboxMessage> Outbox => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}

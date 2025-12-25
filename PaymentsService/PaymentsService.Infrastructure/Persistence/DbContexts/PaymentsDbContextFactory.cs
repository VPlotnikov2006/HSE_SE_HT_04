using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PaymentsService.Infrastructure.Persistence.DbContexts;

public class OrdersDbContextFactory : IDesignTimeDbContextFactory<PaymentsDbContext>
{
    public PaymentsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PaymentsDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=payments_dev;Username=postgres;Password=postgres",
            o => o.MigrationsAssembly("Payments.Infrastructure")
        );

        return new PaymentsDbContext(optionsBuilder.Options);
    }
}

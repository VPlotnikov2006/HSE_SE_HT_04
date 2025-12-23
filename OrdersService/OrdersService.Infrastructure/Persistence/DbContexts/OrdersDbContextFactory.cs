using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrdersService.Infrastructure.Persistence.DbContexts;

public class OrdersDbContextFactory : IDesignTimeDbContextFactory<OrdersDbContext>
{
    public OrdersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=orders_dev;Username=postgres;Password=postgres",
            o => o.MigrationsAssembly("OrdersService.Infrastructure")
        );

        return new OrdersDbContext(optionsBuilder.Options);
    }
}

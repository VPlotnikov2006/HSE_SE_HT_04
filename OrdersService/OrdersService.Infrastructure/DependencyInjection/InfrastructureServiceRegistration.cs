using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Application.Interfaces.Messaging;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Infrastructure.Daemons;
using OrdersService.Infrastructure.Messaging.Kafka;
using OrdersService.Infrastructure.Persistence.DbContexts;
using OrdersService.Infrastructure.Persistence.Repositories;
using OrdersService.Infrastructure.Persistence.UnitOfWork;

namespace OrdersService.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddOrdersServiceInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrdersDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("OrdersDb"),
                npgsql =>
                {
                    npgsql.MigrationsAssembly("OrdersService.Infrastructure");
                }
            );
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Configure<KafkaOptions>(configuration.GetSection("Kafka"));

        services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
        services.AddSingleton<IMessageConsumer, KafkaMessageConsumer>();

        services.AddHostedService<OutboxPublisherDaemon>();
        services.AddHostedService<PaymentStatusConsumerDaemon>();

        return services;
    }
}

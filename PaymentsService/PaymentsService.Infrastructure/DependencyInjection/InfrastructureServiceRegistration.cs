using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentsService.Application.Interfaces.Messaging;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Infrastructure.Daemons;
using PaymentsService.Infrastructure.Messaging.Kafka;
using PaymentsService.Infrastructure.Persistence.DbContexts;
using PaymentsService.Infrastructure.Persistence.Repositories;
using PaymentsService.Infrastructure.Persistence.UnitOfWork;

namespace PaymentsService.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddPaymentsInfrastructure(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddDbContext<PaymentsDbContext>(options =>
        {
            options.UseNpgsql(
                config.GetConnectionString("PaymentsDb"),
                npgsql =>
                {
                    npgsql.MigrationsAssembly("PaymentsService.Infrastructure");
                }
            );
        });

        services.Configure<KafkaOptions>(config.GetSection("Kafka"));

        services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
        services.AddSingleton<IMessageConsumer, KafkaMessageConsumer>();

        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IInboxRepository, InboxRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddHostedService<OutboxPublisherDaemon>();
        services.AddHostedService<OrdersOutboxConsumerDaemon>();

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using PaymentsService.Application.UseCases;

namespace PaymentsService.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddPaymentsApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateAccountHandler>();

        services.AddScoped<DepositHandler>();
        services.AddScoped<GetBalanceHandler>();

        services.AddScoped<WithdrawHandler>();

        return services;
    }
}


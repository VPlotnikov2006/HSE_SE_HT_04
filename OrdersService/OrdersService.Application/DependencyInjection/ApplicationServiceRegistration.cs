using Microsoft.Extensions.DependencyInjection;
using OrdersService.Application.UseCases.CreateOrder;
using OrdersService.Application.UseCases.GetOrder;
using OrdersService.Application.UseCases.ListOrders;
using OrdersService.Application.UseCases.UpdateOrderStatus;

namespace OrdersService.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddOrdersApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateOrderHandler>();

        services.AddScoped<GetOrderHandler>();
        services.AddScoped<ListOrdersHandler>();

        services.AddScoped<UpdateOrderStatusHandler>();

        return services;
    }
}

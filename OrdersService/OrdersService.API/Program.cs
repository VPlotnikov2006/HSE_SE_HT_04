using Microsoft.EntityFrameworkCore;
using OrdersService.Api.Middleware;
using OrdersService.Application.DependencyInjection;
using OrdersService.Infrastructure.Daemons;
using OrdersService.Infrastructure.DependencyInjection;
using OrdersService.Infrastructure.Persistence.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOrdersServiceInfrastructure(builder.Configuration);
builder.Services.AddOrdersApplication();

builder.Services.AddHostedService<OutboxPublisherDaemon>();
builder.Services.AddHostedService<PaymentStatusConsumerDaemon>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using OrdersService.Api.Middleware;
using OrdersService.Application.DependencyInjection;
using OrdersService.Infrastructure.Daemons;
using OrdersService.Infrastructure.DependencyInjection;
using OrdersService.Infrastructure.Persistence.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOrdersServiceInfrastructure(builder.Configuration);
builder.Services.AddOrdersApplication();

builder.Services.AddHostedService<OutboxPublisherDaemon>();
builder.Services.AddHostedService<PaymentStatusConsumerDaemon>();

var app = builder.Build();

// Apply migrations only when explicitly requested (prevents host-side DNS failures
// when the connection string points to a Docker service name like "postgres").
var applyMigrations = Environment.GetEnvironmentVariable("APPLY_MIGRATIONS") == "true";
if (applyMigrations)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    try
    {
        db.Database.Migrate();
        Console.WriteLine("[MIGRATE] applied migrations successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[MIGRATE ERROR] {ex.GetType().Name}: {ex.Message}");
    }
}
else
{
    Console.WriteLine("[MIGRATE] skipped (set APPLY_MIGRATIONS=true to enable)");
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using PaymentsService.Api.Middleware;
using PaymentsService.Application.DependencyInjection;
using PaymentsService.Infrastructure.Daemons;
using PaymentsService.Infrastructure.DependencyInjection;
using PaymentsService.Infrastructure.Persistence.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPaymentsInfrastructure(builder.Configuration);
builder.Services.AddPaymentsApplication();

builder.Services.AddHostedService<OutboxPublisherDaemon>();
builder.Services.AddHostedService<OrdersOutboxConsumerDaemon>();

var app = builder.Build();

// Apply migrations only when explicitly requested (prevents host-side DNS failures
// when the connection string points to a Docker service name like "postgres").
var applyMigrations = Environment.GetEnvironmentVariable("APPLY_MIGRATIONS") == "true";
if (applyMigrations)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
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

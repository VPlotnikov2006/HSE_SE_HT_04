var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
       .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapReverseProxy();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/orders/v1/swagger.json", "Orders API");
    c.SwaggerEndpoint("/swagger/payments/v1/swagger.json", "Payments API");
});

app.Run();

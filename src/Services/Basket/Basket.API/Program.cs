using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

#region Application Services.
// Add services to the container.
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter(); // Using for the API endpoints.

// Add MediatR service.
builder.Services.AddMediatR(config => // MediatR is a library that helps with CQRS(Command and Query Responsibility Segregation)
{
    config.RegisterServicesFromAssemblies(assembly);
    // Register custom behavior.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
#endregion

#region Data Services.
// Add Marten service.
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DBConnString")!);
    // Override the default identity value and set UserName as identity column.
    options.Schema.For<ShoppingCart>().Identity(s => s.UserName);
}).UseLightweightSessions(); // Session chooses the performance of the database.

// Add IBasketRepository.
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
#endregion

#region Grpc Services.
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});
#endregion

#region Cross-Cutting Services.
// Register custom exception.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("DBConnString")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
#endregion

var app = builder.Build();

app.UseExceptionHandler(options => { }); // Empty suggest to use registerd custom exception handler.

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

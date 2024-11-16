var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.

// Add services to the container.
builder.Services.AddCarter(); // Using for the API endpoints.

// Add MediatR service.
builder.Services.AddMediatR(config => // MediatR is a library that helps with CQRS(Command and Query Responsibility Segregation)
{
    config.RegisterServicesFromAssemblies(assembly);
    // Register custom behavior.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

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

// Register custom exception.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(options => { }); // Empty suggest to use registerd custom exception handler.

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

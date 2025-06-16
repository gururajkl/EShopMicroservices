using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.

// Register CustomException.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Register carter.
builder.Services.AddCarter();

// Register MedaitR.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Register Marten.
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName); // Set UserName as identity field.
}).UseLightweightSessions();

// Register Basket service.
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>(); // Using Scrutor to create a decorator of BasketRepo.

// Register StackExchange redis cache.
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("Redis"); // Default is 6379.
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// Handler global exception.
// Empty indicates that to rely on custom exception handler.
app.UseExceptionHandler(options => { });

app.MapCarter();

app.Run();

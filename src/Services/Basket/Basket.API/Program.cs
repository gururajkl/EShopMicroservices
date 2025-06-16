var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();

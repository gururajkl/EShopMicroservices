var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add MediatR service.
builder.Services.AddMediatR(config => // MediatR is a library that helps with CQRS(Command and Query Responsibility Segregation)
{
    config.RegisterServicesFromAssemblies(assembly);
    // Register custom behavior.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add Validator service (Registering).
builder.Services.AddValidatorsFromAssembly(assembly);

// Add services to the container.
builder.Services.AddCarter(); // Using for the API endpoints.

// Add Marten service.
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DBConnString")!);
}).UseLightweightSessions(); // Session chooses the performance of the database.

// Register custom exception.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { }); // Empty suggest to use registerd custom exception handler.

app.Run();

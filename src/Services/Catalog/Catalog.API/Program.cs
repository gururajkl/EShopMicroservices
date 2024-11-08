var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add MediatR service.
builder.Services.AddMediatR(config => // MediatR is a library that helps with CQRS(Command and Query Responsibility Segregation)
{
    config.RegisterServicesFromAssemblies(assembly);
    // Register custom behavior.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
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

// Initialize some seed data if the environment is development.
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

// Register custom exception.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add HealthCheck service.
// Check health of postgreSql as well, for that use: AspNetCore.HealthChecks.NpgSql
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("DBConnString")!);

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { }); // Empty suggest to use registerd custom exception handler.

// Configure health check pipeline.
// Configure UI of the health check using AspNetCore.HealthChecks.UI.Client.
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

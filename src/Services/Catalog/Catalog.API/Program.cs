var builder = WebApplication.CreateBuilder(args);

#region Before building app, Add services to the container.
var assesmbly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => // Resgiter MediatR.
{
    config.RegisterServicesFromAssemblies(assesmbly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Register custom pipeline behavior.
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); // Register custom pipeline behavior.
});

builder.Services.AddValidatorsFromAssembly(assesmbly); // Register Fluent validation.

builder.Services.AddCarter(); // Register Carter.

builder.Services.AddMarten(options => // Register Marten.
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

// Register custom exception handler.
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

var app = builder.Build();

#region After building app, Configure HTTP request pipeline.
app.MapCarter(); // Helps to map the routes, adds to the middleware.

// Handler global exception.
// Empty indicates that to rely on custom exception handler.
app.UseExceptionHandler(option => { });
#endregion

app.Run();

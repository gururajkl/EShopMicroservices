using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Handle global exception.
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null)
        {
            return;
        }

        var problemDetails = new ProblemDetails()
        {
            Title = exception.Message,
            Detail = exception.StackTrace,
            Status = StatusCodes.Status500InternalServerError
        };

        var logger = context.RequestServices.GetService<ILogger<Program>>();
        logger?.LogError(exception, exception.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

app.Run();

using BuildingBlocks.Behaviors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

#region Before building app, Add services to the container.
var assesmbly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => // Resgiter MediatR.
{
    config.RegisterServicesFromAssemblies(assesmbly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // Register custom pipeline behavior.
});

builder.Services.AddValidatorsFromAssembly(assesmbly); // Register Fluent validation.

builder.Services.AddCarter(); // Register Carter.

builder.Services.AddMarten(options => // Register Marten.
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
#endregion

var app = builder.Build();

#region After building app, Configure HTTP request pipeline.
app.MapCarter(); // Helps to map the routes, adds to the middleware.

// Handler global exception.
app.UseExceptionHandler(exceptionHandler =>
{
    exceptionHandler.Run(async (HttpContext context) =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null) return;

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception?.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});
#endregion

app.Run();

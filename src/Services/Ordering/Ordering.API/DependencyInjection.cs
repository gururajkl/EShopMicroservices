using BuildingBlocks.Exceptions.Handler;

namespace Ordering.API;

public static class DependencyInjection
{
    /// <summary>
    /// Service collection extension method that registers required service for API layer.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> object.</param>
    /// <returns>Service object with all added services.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }

    // After buidling the app.
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        return app;
    }
}

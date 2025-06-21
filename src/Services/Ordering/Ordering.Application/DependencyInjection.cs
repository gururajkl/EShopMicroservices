using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Service collection extension method that registers required service for Application layer.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> object.</param>
    /// <returns>Service object with all added services.</returns>
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        return services;
    }
}

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Service collection extension method that registers required service for Infrastructure layer.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> object.</param>
    /// <returns>Service object with all added services.</returns>
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        // Add services to the container.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString);
            // Register interceptors for EFCore.
            options.AddInterceptors(serviceProvider.GetService<ISaveChangesInterceptor>()!);
        });

        return services;
    }
}

using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add service to the container.
// Add service injections using extension method.

// Infrastructure - Ef core.
// Appliacation - MediatR.
// API - Carter, Health checks...
// These are all extension methods.
builder.Services.AddApplicationService().AddInfrastructureService(builder.Configuration).AddApiServices();

var app = builder.Build();

// After building the app.

// Custom extension method.
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialDatabaseAsync();
}

app.Run();

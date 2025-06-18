using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register DbContext using SQLLite connection string from configuration.
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

app.UseMigration(); // Apply pending migrations at startup.

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.Run();

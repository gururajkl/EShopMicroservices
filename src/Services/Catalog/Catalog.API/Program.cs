var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter(); // Using for the API endpoints.
builder.Services.AddMediatR(config => // MediatR is a library that helps with CQRS(Command and Query Responsibility Segregation)
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Setup the YARP middleware.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipelines.
app.MapReverseProxy();

app.Run();

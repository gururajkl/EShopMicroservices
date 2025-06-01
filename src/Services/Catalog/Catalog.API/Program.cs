var builder = WebApplication.CreateBuilder(args);

#region Before building app, Add services to the container.
builder.Services.AddCarter(); // Register Carter.
builder.Services.AddMediatR(config => // Resgiter MediatR.
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
#endregion

var app = builder.Build();

#region After building app, Configure HTTP request pipeline.
app.MapCarter(); // Helps to map the routes, adds to the middleware.
#endregion

app.Run();

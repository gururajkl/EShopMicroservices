var builder = WebApplication.CreateBuilder(args);

// Before building app, Add services to the container.

var app = builder.Build();

// After building app, Configure HTTP request pipeline.

app.Run();

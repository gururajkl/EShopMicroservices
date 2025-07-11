var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register Refit.
builder.Services.AddRefitClient<ICatalogService>().ConfigureHttpClient(configuration =>
{
    configuration.BaseAddress = new Uri(builder.Configuration["ApiSettings:GateWayAddress"]!);
});
builder.Services.AddRefitClient<IBasketServices>().ConfigureHttpClient(configuration =>
{
    configuration.BaseAddress = new Uri(builder.Configuration["ApiSettings:GateWayAddress"]!);
});
builder.Services.AddRefitClient<IOrderingService>().ConfigureHttpClient(configuration =>
{
    configuration.BaseAddress = new Uri(builder.Configuration["ApiSettings:GateWayAddress"]!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

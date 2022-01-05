using CryptocurrencyPrice.Interface;
using CryptocurrencyPrice.Middleware;
using CryptocurrencyPrice.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ICryptocurrencyService, CryptocurrencyService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CryptocurrencyApi"));
    client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration.GetValue<string>("X-CMC_PRO_API_KEY"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{symbols?}/{amount?}/{id?}");
app.MapFallbackToFile("index.html"); ;
app.Run();
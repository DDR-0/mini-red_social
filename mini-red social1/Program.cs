using System.Net;
using Microsoft.EntityFrameworkCore;
using mini_red_social1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<socialDBcontext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "Connection string 'DefaultConnection' was not found.")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<socialDBcontext>();
    database.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Reemplaza la página 404 del navegador por nuestra vista amigable.
app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

// Interceptamos el host antes del enrutamiento para que un subdominio
// desconocido nunca llegue a una acción normal de la aplicación.
app.Use(async (context, next) =>
{
    var host = context.Request.Host.Host.TrimEnd('.');
    var labels = host.Split('.', StringSplitOptions.RemoveEmptyEntries);
    var isIpAddress = IPAddress.TryParse(host, out _);

    // "foo.localhost" se trata como un subdominio en desarrollo.
    var subdomain = host.EndsWith(".localhost", StringComparison.OrdinalIgnoreCase)
        ? host[..^".localhost".Length]
        : !isIpAddress && labels.Length >= 3
            ? string.Join('.', labels[..^2])
            : null;

    var allowedSubdomains = builder.Configuration
        .GetSection("Subdomains:Allowed")
        .Get<string[]>() ?? Array.Empty<string>();

    if (!string.IsNullOrWhiteSpace(subdomain) &&
        !allowedSubdomains.Contains(subdomain, StringComparer.OrdinalIgnoreCase))
    {
        // Marcamos la petición y la enviamos a Home/Error sin redirigir:
        // así conservamos el host original y evitamos un bucle infinito.
        context.Items["InvalidSubdomain"] = true;
        context.Request.Path = "/Home/Error";
    }

    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
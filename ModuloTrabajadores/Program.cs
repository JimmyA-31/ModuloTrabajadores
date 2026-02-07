using Microsoft.EntityFrameworkCore;
using ModuloTrabajadores.Data;
using ModuloTrabajadores.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar EF Core con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
                     new MySqlServerVersion(new Version(8, 0, 44)))); // ajusta tu versión

// Servicios
builder.Services.AddScoped<ITrabajadorService, TrabajadorService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trabajadores}/{action=Index}/{id?}");

app.Run();

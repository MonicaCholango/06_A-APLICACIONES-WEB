using ExamenParcial.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cliente}/{action=Index}/{id?}"); 

app.Run();

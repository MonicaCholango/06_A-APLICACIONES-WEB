using Microsoft.EntityFrameworkCore;
using variables.Config;

var builder = WebApplication.CreateBuilder(args);

var cn = builder.Configuration.GetConnectionString("cn");

builder.Services.AddDbContext<ejemplodbcontext>(opciones => opciones.UseSqlServer(cn));


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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

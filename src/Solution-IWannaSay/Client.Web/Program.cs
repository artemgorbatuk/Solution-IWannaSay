using Client.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container

builder.Services.AddControllersWithViews();

builder.Services.AddDependencyInjection();

builder.Services.AddHubConnection();

var app = builder.Build();

#endregion

#region Configure the HTTP request pipeline

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion
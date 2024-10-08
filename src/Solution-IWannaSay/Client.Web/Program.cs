using Client.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

var uri = builder.Environment.IsDevelopment()
    ? new Uri("http://localhost:6000/")
    : new Uri("http://signalr:6000/");

#region Add services to the container

//builder.Configuration.AddJsonFileExt(builder.Environment);

builder.Services.AddDbContextExt(builder.Configuration);

//builder.Services.AddDatabaseMigrationExt();

builder.Services.AddDependencyInjectionExt();

builder.Services.AddAuthenticationExt();

//builder.Services.AddAuthorizationExt();

builder.Services.AddCorsExt(uri);

builder.Services.AddHubConnectionExt(uri);

builder.Services.AddControllersWithViews();

//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options => {
//    options.IdleTimeout = TimeSpan.FromSeconds(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

var app = builder.Build();

#endregion

#region Configure the HTTP request pipeline

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

//app.UseStaticFilesExt(app.Environment.IsDevelopment(), builder.Environment.ContentRootPath);

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

//app.UseSession();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();

#endregion
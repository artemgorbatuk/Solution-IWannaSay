using Datasource.SignalR.Api;

var builder = WebApplication.CreateBuilder(args);

var uri = builder.Environment.IsDevelopment()
    ? new Uri("http://localhost:5000/")
    : new Uri("http://client:5000/");

//builder.Configuration.AddJsonFileExt(builder.Environment);

builder.Services.AddSignalR();

builder.Services.AddLogging(logging => {
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => {
        builder.WithOrigins(uri.AbsoluteUri)
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

var app = builder.Build();

app.UseRouting();

app.UseCors("CorsPolicy");

app.MapHub<ChatHub>("/notification");

await app.RunAsync();
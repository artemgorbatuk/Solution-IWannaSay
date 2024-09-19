using Datasource.SignalR.Api;

var builder = WebApplication.CreateBuilder(args);

var uri = builder.Environment.IsDevelopment()
    ? new Uri("http://localhost:5000/")
    : new Uri("http://client:5000/");

//builder.Services.AddSignalRCore();
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

app.UseRouting();

app.MapHub<ChatHub>("/notification");

//app.MapHub<ChatHub>("/notification", 
//    options => { options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling; } );

await app.RunAsync();
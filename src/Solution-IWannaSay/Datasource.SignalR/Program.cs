using Datasource.SignalR.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

//builder.Services.AddSignalRCore();

builder.Services.AddLogging(logging => {
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => {
        builder.WithOrigins("http://localhost:5000")
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseRouting();

app.MapHub<ChatHub>("/notification");

//app.MapHub<ChatHub>("/notification", 
//    options => { options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling; } );

await app.RunAsync();
using Server.SignalR.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

//builder.Services.AddSignalRCore();

builder.Services.AddLogging(logging => {
    logging.AddConsole();
    logging.AddDebug();
});

var app = builder.Build();

app.UseRouting();

app.MapHub<ChatHub>("/notification");

//app.MapHub<ChatHub>("/notification", 
//    options => { options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling; } );

app.Run();
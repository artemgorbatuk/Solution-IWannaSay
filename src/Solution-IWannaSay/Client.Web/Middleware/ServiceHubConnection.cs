using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Web.Middleware;

public static class ServiceHubConnection {
    public static IServiceCollection AddHubConnection(this IServiceCollection services) {
        return services.AddSingleton(_ =>
            new HubConnectionBuilder()
                .WithUrl("http://localhost:5154/notification")
                //.WithUrl("http://localhost:5154/notification", HttpTransportType.WebSockets | HttpTransportType.LongPolling)
                //.WithStatefulReconnect()
                //.WithAutomaticReconnect()
                //.AddMessagePackProtocol()
                .Build());
    }
}
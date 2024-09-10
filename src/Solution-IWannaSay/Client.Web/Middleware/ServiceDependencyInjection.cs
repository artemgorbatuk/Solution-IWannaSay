using Repositories.SignalR.Api;
using Repositories.SignalR.Interfaces;
using Services.Api;
using Services.Interfaces;

namespace Client.Web.Middleware;

public static class ServiceDependencyInjection {
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services) {

        services.AddScoped<IConnectorRoom, ConnectorRoom>();
        services.AddScoped<IConnectorMessage, ConnectorMessage>();

        services.AddScoped<IServiceRoom, ServiceRoom>();
        services.AddScoped<IServiceMessage, ServiceMessage>();

        return services;
    }
}
using Datasource.Ef.Contexts;
using Datasource.Ef.Factories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.Ef.Api;
using Repositories.Ef.Interfaces;
using Repositories.SignalR.Api;
using Repositories.SignalR.Interfaces;
using Services.Api;
using Services.Interfaces;

namespace Client.Web.Middleware;

public static class ServiceRegistration {
    public static IServiceCollection AddDependencyInjectionExt(this IServiceCollection services) {

        services.AddScoped<IServiceAuthentification, ServiceAuthentification>();
        services.AddScoped<IServiceRoom, ServiceRoom>();
        services.AddScoped<IServiceMessage, ServiceMessage>();

        services.AddScoped<IRepositoryAuthentication, RepositoryAuthentication>();

        services.AddScoped<IConnectorRoom, ConnectorRoom>();
        services.AddScoped<IConnectorMessage, ConnectorMessage>();

        services.AddSingleton<IServiceScopeFactory, DbContextFactoryScope>();
        services.AddSingleton<IDesignTimeDbContextFactory<DbContextIWannaSay>, DbContextFactoryDesignTime>();

        return services;
    }

    public static IServiceCollection AddHubConnectionExt(this IServiceCollection services) {
        return services.AddSingleton(_ =>
            new HubConnectionBuilder()
                .WithUrl("http://localhost:6000/notification")
                //.WithUrl("http://localhost:6000/notification", HttpTransportType.WebSockets | HttpTransportType.LongPolling)
                //.WithStatefulReconnect()
                //.WithAutomaticReconnect()
                //.AddMessagePackProtocol()
                .Build());
    }

    public static IServiceCollection AddDbContextExt(this IServiceCollection services, ConfigurationManager configuration) {
        // ДЛЯ СОЗДАНИЯ ПОТОКОБЕЗОПАСНОГО DbContext ТРЕБУЕТСЯ:
        // 1 создать DbContextFactorySequentialSearch с жизненным циклом Singleton отнаследованного от IServiceScopeFactory
        // 2 зарегистрировать DbContext c жизненным циклом Scoped
        // 3 получение контекста в Repository будет происходить через IServiceScopeFactory (к сожалению антипаттерн)
        // 4 Repository и Service должны быть зарегистрированы как AddScoped

        services.AddDbContext<DbContextIWannaSay>(options =>
        {
            if (!options.IsConfigured) {
                // во время запуска тестов без этой проверки
                // возникает ошибка 'Multiple database providers in service provider'.
                // InMemory создает свою конфигурацию.

                var connectionString = configuration.GetConnectionString("Docker") ?? default!;

                options.UseSqlServer(connectionString);
            }
        }, ServiceLifetime.Scoped);

        return services;
    }

    public static IServiceCollection AddAuthenticationExt(this IServiceCollection services) {

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.Cookie.Name = "Auth";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = new PathString("/Account/Login");
                options.LogoutPath = new PathString("/Account/Logout");
            });

        return services;
    }

    public static IServiceCollection AddAuthorizationExt(this IServiceCollection services) {

        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        services.AddAuthorization(options => {
            options.DefaultPolicy = policy;
            options.AddPolicy("AuthPolicy", policy);
        });

        return services;
    }

    public static IServiceCollection AddCorsExt(this IServiceCollection services) {

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyOrigin()
                //.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                //.AllowCredentials()
                //The CORS protocol does not allow 
                // specifying a wildcard (any) origin and credentials at the same time.
                // Configure the CORS policy by listing individual origins
                // if credentials needs to be supported.
                .SetIsOriginAllowedToAllowWildcardSubdomains());
        });

        return services;
    }

}
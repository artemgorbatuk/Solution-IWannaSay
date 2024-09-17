namespace Client.Web.Middleware;

public static class ConfigurationRegistration {
    public static ConfigurationManager AddJsonFileExt(this ConfigurationManager configuration, IWebHostEnvironment environment) {

        if (environment.IsProduction()) {

            var appsettings = Path.Combine(environment.ContentRootPath, "app", "client", "appsettings.json");

            configuration.AddJsonFile(appsettings, optional: false, reloadOnChange: true);
        }

        return configuration;
    }
}
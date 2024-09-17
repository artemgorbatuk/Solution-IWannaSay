using Microsoft.Extensions.FileProviders;

namespace Client.Web.Middleware;

public static class ApplicationConfigure {
    public static IApplicationBuilder UseStaticFilesExt(this IApplicationBuilder app, bool IsDevelopment, string contentRootPath) {

        if (IsDevelopment) {
            app.UseStaticFiles();
        }
        else {

            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    // there is a deploing path from Dockerfile
                    Path.Combine(contentRootPath, "app", "client", "wwwroot")
                ),
                RequestPath = ""
            });
        }

        return app;
    }
}
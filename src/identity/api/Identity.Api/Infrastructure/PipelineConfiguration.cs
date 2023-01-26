using Microsoft.OpenApi.Models;

namespace eLibraryOnContainers.Identity.Api.Infrastructure;

public static class PipelineConfiguration
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                {
                    if (!httpRequest.Headers.ContainsKey("X-Forwarded-Host") ||
                        !httpRequest.Headers.ContainsKey("X-Forwarded-Prefix") ||
                        !httpRequest.Headers.ContainsKey("X-Forwarded-Proto"))
                    {
                        return;
                    }
                    var basePath = httpRequest.Headers["X-Forwarded-Prefix"];
                    var host = httpRequest.Headers["X-Forwarded-Host"];
                    var protocol = httpRequest.Headers["X-Forwarded-Proto"];
                    var serverUrl = $"{protocol}://{host}/{basePath}";

                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = serverUrl },
                    };
                });
            });
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        return app;
    }
}
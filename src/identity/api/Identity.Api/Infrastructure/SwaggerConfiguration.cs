using Microsoft.OpenApi.Models;

namespace eLibraryOnContainers.Identity.Api.Infrastructure;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        return services.AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eLibraryOnContainers.Identity", Version = "v1" });

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        Description = "Authorization token",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                    });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
            });
    }
}
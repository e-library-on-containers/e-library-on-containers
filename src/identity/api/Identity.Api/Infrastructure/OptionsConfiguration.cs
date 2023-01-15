using eLibraryOnContainers.Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace eLibraryOnContainers.Identity.Api.Infrastructure;

public static class OptionsConfiguration
{
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<AuthOptions>(configuration.GetAuthSection())
            .AddSingleton<IOptions<IAuthOptions>>(provider => provider.GetRequiredService<IOptions<AuthOptions>>())
            .Configure<SqlOptions>(options =>
                options.ConnectionString = configuration.GetConnectionString("DefaultConnection"))
            .AddSingleton<IOptions<ISqlOptions>>(provider => provider.GetRequiredService<IOptions<SqlOptions>>());

    private static IConfiguration GetAuthSection(this IConfiguration configuration) =>
        configuration.GetSection("AuthOptions");

    public static AuthOptions GetAuthOptions(this IConfiguration configuration) =>
        configuration.GetAuthSection().Get<AuthOptions>();
}
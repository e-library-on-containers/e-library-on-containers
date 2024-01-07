using Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Identity.Api.Infrastructure;

public static class OptionsConfiguration
{
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<AuthOptions>(configuration.GetAuthSection())
            .AddSingleton<IOptions<IAuthOptions>>(provider => provider.GetRequiredService<IOptions<AuthOptions>>())
            .Configure<SqlOptions>(options =>
                options.ConnectionString = configuration.GetConnectionString("DefaultConnection"))
            .AddSingleton<IOptions<ISqlOptions>>(provider => provider.GetRequiredService<IOptions<SqlOptions>>())
            .Configure<ConsulOptions>(configuration.GetConsulSection())
            .AddSingleton<IOptions<IConsulOptions>>(provider => provider.GetRequiredService<IOptions<ConsulOptions>>());

    private static IConfiguration GetAuthSection(this IConfiguration configuration) =>
        configuration.GetSection("AuthOptions");

    public static AuthOptions GetAuthOptions(this IConfiguration configuration) =>
        configuration.GetAuthSection().Get<AuthOptions>();
    
    private static IConfiguration GetConsulSection(this IConfiguration configuration) =>
        configuration.GetSection("ConsulOptions");
    public static ConsulOptions GetConsulOptions(this IConfiguration configuration) =>
        configuration.GetConsulSection().Get<ConsulOptions>();
}
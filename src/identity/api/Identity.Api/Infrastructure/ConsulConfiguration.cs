using Consul;
using Identity.Api.Services;
using Identity.Infrastructure.Options;

namespace Identity.Api.Infrastructure;

public static class ConsulConfiguration
{
    public static IServiceCollection AddConsulServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetConsulOptions();
        if (options is not { UseConsul: true })
        {
            return services;
        }
        
        var consulClient = CreateConsulClient(options);
        services.AddSingleton(options);
        services.AddSingleton<IHostedService, ConsulHostedService>();
        services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);

        return services;
    }

    private static ConsulClient CreateConsulClient(IConsulOptions serviceConfig)
    {
        return new ConsulClient(config => { config.Address = serviceConfig.ServiceDiscoveryAddress; });
    }
}
using Consul;

namespace Movies.Api.Consul;

public static class ConsulConfiguration
{
    public static IServiceCollection AddConsulServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetConsulOptions();
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
    
    public static IConfiguration GetConsulSection(this IConfiguration configuration) =>
        configuration.GetSection("ConsulOptions");
    public static ConsulOptions GetConsulOptions(this IConfiguration configuration) =>
        configuration.GetConsulSection().Get<ConsulOptions>()!;
}
using Consul;
using Microsoft.Extensions.Options;

namespace Books.Api.Consul;

public class ConsulHostedService : IHostedService
{
    private readonly IConsulClient _client;
    private readonly IConsulOptions _config;
    private string? _registrationId;

    public ConsulHostedService(IOptions<IConsulOptions> config, IConsulClient client)
    {
        _client = client;
        _config = config.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _registrationId = $"{_config.ServiceName}-{_config.ServiceId}";
        var registration = new AgentServiceRegistration
        {
            ID = _registrationId,
            Name = _config.ServiceName,
            Address = _config.ServiceAddress.Host,
            Port = _config.ServiceAddress.Port
        };
        
        await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
        await _client.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
    }
}
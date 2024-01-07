namespace Books.Api.Consul;

public interface IConsulOptions
{
    Uri ServiceDiscoveryAddress { get; set; }
    Uri ServiceAddress { get; set; }
    string ServiceName { get; set; }
    string ServiceId { get; set; }
}

public class ConsulOptions : IConsulOptions
{
    public Uri ServiceDiscoveryAddress { get; set; } = null!;
    public Uri ServiceAddress { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
}
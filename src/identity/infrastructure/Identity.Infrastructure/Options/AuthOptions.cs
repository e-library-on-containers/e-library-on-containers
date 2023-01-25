namespace Identity.Infrastructure.Options;

public interface IAuthOptions
{
    public string Key { get; }
    public int ExpiredInMinutes { get; }
}

public class AuthOptions : IAuthOptions
{
    public string Key { get; set; } = null!;
    public int ExpiredInMinutes { get; set; }
}
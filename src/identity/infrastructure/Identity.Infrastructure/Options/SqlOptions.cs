namespace eLibraryOnContainers.Identity.Infrastructure.Options;

public interface ISqlOptions
{
    public string ConnectionString { get; }
}

public class SqlOptions : ISqlOptions
{
    public string ConnectionString { get; set; } = null!;
}
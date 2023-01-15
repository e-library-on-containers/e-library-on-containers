using eLibraryOnContainers.Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace eLibraryOnContainers.Identity.Infrastructure.Common;

public interface ISqlConnectionFactory
{
    Task<NpgsqlConnection> CreateAsync();

}
public class NpgsqlConnectionFactory : ISqlConnectionFactory
{
    private readonly SqlOptions _options;

    public NpgsqlConnectionFactory(IOptions<SqlOptions> options)
    {
        _options = options.Value;
    }

    public async Task<NpgsqlConnection> CreateAsync()
    {
        var conn = new NpgsqlConnection(_options.ConnectionString);

        await conn.OpenAsync();

        return conn;
    }
}

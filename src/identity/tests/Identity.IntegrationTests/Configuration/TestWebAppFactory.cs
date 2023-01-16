using eLibraryOnContainers.Identity.Infrastructure.Common;
using eLibraryOnContainers.Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace eLibraryOnContainers.Identity.IntegrationTests.Configuration;

internal class TestWebAppFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public TestWebAppFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Tests.json")
                    .Build());
            })
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(ISqlConnectionFactory));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddScoped<ISqlConnectionFactory>(_ => new NpgsqlConnectionFactory(Options.Create(
                    new SqlOptions
                    {
                        ConnectionString = _connectionString
                    })));
            });

        base.ConfigureWebHost(builder);
    }
}
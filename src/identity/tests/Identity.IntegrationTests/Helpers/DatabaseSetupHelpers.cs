using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Identity.Database;

namespace Identity.IntegrationTests.Helpers;

public static class DatabaseSetupHelpers
{
    public static async Task<PostgreSqlTestcontainer> StartContainerAsync()
    {
        var container = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "integration-test-db",
                Username = "identity",
                Password = "identity-password"
            }).Build();

        await container.StartAsync();

        Upgrader.Upgrade(container.ConnectionString);

        return container;
    }
}
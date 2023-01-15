using Dapper;
using DotNet.Testcontainers.Containers;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using eLibraryOnContainers.Identity.Infrastructure.Common;
using eLibraryOnContainers.Identity.Infrastructure.Models;
using eLibraryOnContainers.Identity.Infrastructure.Options;
using eLibraryOnContainers.Identity.Infrastructure.Repositories;
using eLibraryOnContainers.Identity.IntegrationTests.Helpers;
using Machine.Specifications;
using Microsoft.Extensions.Options;
using Npgsql;

namespace eLibraryOnContainers.Identity.IntegrationTests;

public class TestTest
{
    private static PostgreSqlTestcontainer container;

    private Establish ctx = async () =>
    {
        container = await DatabaseSetupHelpers.StartContainerAsync();
    };

    //private It should = async () =>
    //{
    //    //var conn = new NpgsqlConnection(container.ConnectionString);
        
    //    //await conn.OpenAsync();

    //    var opts = Options.Create(new SqlOptions()
    //    {
    //        ConnectionString = container.ConnectionString
    //    });
    //    var repo = new UsersRepository(new NpgsqlConnectionFactory(opts));

    //    var res = await repo.GetUserByEmailAsync(Email.From("admin@domain.com").Value);

    //    //var res = await conn.QueryAsync<ReadUser>("SELECT * FROM users");

    //    var a = 1;
    //};

    private Cleanup after = async () => await container.StopAsync();

}
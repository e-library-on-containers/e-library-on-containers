using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Books.Infrastructure.Repositories;

public class PeopleRepository : IRepository<Person>
{
    private readonly IConfiguration configuration;

    public PeopleRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            return await connection.QueryAsync<Person>("SELECT * FROM People");
        }
    }

    public async Task<Person> GetById(int id)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Person>("SELECT * FROM People WHERE Id = @Id", new { Id = id });
        }
    }

    public async Task<int> Add(Person person)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            return await connection.ExecuteAsync("INSERT INTO People (Surname, Name) VALUES (@Surname, @Name)", person);
        }
    }

    public async Task Update(Person person)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            await connection.ExecuteAsync("UPDATE People SET Surname = @Surname, Name = @Name WHERE Id = @Id", person);
        }
    }

    public async Task Delete(int id)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            await connection.ExecuteAsync("DELETE FROM People WHERE Id = @Id", new { Id = id });
        }
    }
}
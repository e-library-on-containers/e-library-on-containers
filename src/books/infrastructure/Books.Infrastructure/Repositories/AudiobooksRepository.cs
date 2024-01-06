using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Books.Infrastructure.Repositories;

public class AudiobooksRepository : IRepository<Audiobook>
{
    private readonly IConfiguration configuration;

    public AudiobooksRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<Audiobook>> GetAll()
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            return await connection.QueryAsync<Audiobook>("SELECT * FROM Audiobooks");
        }
    }

    public async Task<Audiobook> GetById(int id)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Audiobook>("SELECT * FROM Audiobooks WHERE Id = @Id", new { Id = id });
        }
    }

    public async Task<int> Add(Audiobook audiobook)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            return await connection.ExecuteAsync("INSERT INTO Audiobooks (BookId, Duration) VALUES (@BookId, @Duration)", audiobook);
        }
    }

    public async Task Update(Audiobook audiobook)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            await connection.ExecuteAsync("UPDATE Audiobooks SET BookId = @BookId, Duration = @Duration, InPreview = @InPreview WHERE Id = @Id", audiobook);
        }
    }

    public async Task Delete(int id)
    {
        using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
        {
            connection.Open();
            await connection.ExecuteAsync("DELETE FROM Audiobooks WHERE Id = @Id", new { Id = id });
        }
    }
}
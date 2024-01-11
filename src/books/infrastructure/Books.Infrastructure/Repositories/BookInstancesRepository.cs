using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Npgsql;

namespace Books.Infrastructure.Repositories
{
    public class BookInstancesRepository : IBookInstancesRepository<BookInstance>
    {
        private readonly IConfiguration configuration;

        public BookInstancesRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> Create(BookInstance _object)
        {
            var sql = "Insert into BookInstances (ISBN, IsAvailable, BookId) VALUES (@ISBN, @IsAvailable, " +
                "(SELECT BookId FROM BooksRead where ISBN = @ISBN))";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, _object);
                return result;
            }
        }

        public async Task<int> Delete(string ISBN)
        {
            var sql = "DELETE FROM BookInstances WHERE ISBN = @ISBN";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { ISBN = ISBN });
                return result;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM BookInstances WHERE InstanceId = @InstanceId";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { InstanceId = id });
                return result;
            }
        }

        public async Task<List<BookInstance>> GetAll()
        {
            var sql = "SELECT * FROM BookInstances b";

            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<BookInstance>(sql);
                return result.ToList();
            }
        }

        public async Task<BookInstance> GetById(int id)
        {
            var sql = "SELECT * FROM BookInstances WHERE InstanceId = @InstanceId";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<BookInstance>(sql, new { InstanceId = id });
                return result;
            }
        }

        public async Task<List<BookInstance>> GetByISBN(string isbn, bool isAvailable)
        {
            var sql = "SELECT * FROM BookInstances WHERE ISBN = @ISBN";
            if (isAvailable)
            {
                sql += " AND isAvailable = true";
            }
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<BookInstance>(sql, new { ISBN = isbn });
                return result.ToList();
            }
        }

        public async Task<Book> GetInfoByInstanceId(int id)
        {
            var sql =
                "SELECT br.* FROM BooksRead br JOIN BookInstances bi ON br.BookId = bi.BookId WHERE bi.InstanceId = @InstanceId;";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Book>(sql, new { InstanceId = id });
                return result.FirstOrDefault()!;
            }
        }

        public async Task<int> Update(BookInstance _object)
        {
            var sql = "UPDATE BookInstances SET ISBN = @ISBN, IsAvailable = @IsAvailable WHERE InstanceId = @InstanceId";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, _object);
                return result;
            }
        }
    }
}

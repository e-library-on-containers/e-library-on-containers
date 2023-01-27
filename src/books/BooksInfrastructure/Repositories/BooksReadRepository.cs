using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class BooksReadRepository : IBookRepository<BookRead>
    {
        private readonly IConfiguration configuration;

        public BooksReadRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> Create(BookRead _object)
        {
            var sql = "Insert into BooksRead (ISBN, Title, Description, Authors, CopiesCount) VALUES (@ISBN, @Title, @Description, @Authors, @CopiesCount)";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, _object);
                return result;
            }
        }

        public Task<int> Delete(string ISBN)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookRead>> GetAll()
        {
            var sql = "SELECT * FROM BooksRead";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<BookRead>(sql);
                return result.ToList();
            }
        }

        public async Task<BookRead> GetById(int id)
        {
            var sql = "SELECT * FROM BooksRead WHERE BookId = @BookId";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<BookRead>(sql, new { BookId = id });
                return result;
            }
        }

        public async Task<BookRead> GetByISBN(string isbn)
        {
            var sql = "SELECT * FROM BooksRead WHERE ISBN = @ISBN";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<BookRead>(sql, new { ISBN = isbn });
                return result;
            }
        }

        public async Task<int> Update(BookRead _object)
        {
            var sql = "UPDATE BooksRead SET Authors = @Authors, ISBN = @ISBN, Title = @Title, Description = @Description, CoverImg = @CoverImg, CopiesCount = @CopiesCount WHERE BookId = @BookId";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, _object);
                return result;
            }
        }
    }
}

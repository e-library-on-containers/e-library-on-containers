using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using Npgsql;

namespace Books.Infrastructure.Repositories
{
    public class BooksRepository : IBookRepository<Book>
    {
        private readonly IRepository<Person> _peopleRepository;
        private readonly IConfiguration configuration;

        public BooksRepository(IRepository<Person> peopleRepository, IConfiguration configuration)
        {
            _peopleRepository = peopleRepository;
            this.configuration = configuration;
        }

        public async Task<int> Create(Book _object)
        {
            var sql = "Insert into Books (ISBN, Title, Description, Authors, CoverImg) VALUES (@ISBN, @Title, @Description, @Authors, @CoverImg)";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, _object);

                foreach (var author in _object.Authors.Split(",").Select(x => x.Trim()))
                { 
                    var parts = author.Split(" ");
                    await _peopleRepository.Add(new Person
                    {
                        Name = parts.First(),
                        Surname = string.Join(' ', parts.Skip(1))
                    });
                }
                
                return result;
            }
        }

        public async Task<int> Delete(string ISBN)
        {
            var sql = "DELETE FROM Books WHERE ISBN = @ISBN";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, new { ISBN = ISBN });
                    return result;
            }
        }

        public async Task<List<Book>> GetAll(bool includePreview = false)
        {
            var sql = "SELECT * FROM Books b";
            
            if (!includePreview)
            {
                sql += " WHERE b.InPreview = 'FALSE'";
            }
            
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Book>(sql);
                return result.ToList();
            }
        }

        public async Task<Book> GetById(int id)
        {
            var sql = "SELECT * FROM Books WHERE BookId = @BookId";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Book>(sql, new { BookId = id });
                return result;
            }
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            var sql = "SELECT * FROM Books WHERE ISBN = @ISBN";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Book>(sql, new { ISBN = isbn });
                return result;
            }
        }

        public async Task<int> Update(Book _object)
        {
            var sql = "UPDATE Books SET Authors = @Authors, ISBN = @ISBN, Title = @Title, Description = @Description, CoverImg = @CoverImg WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, _object);
                return result;
            }
        }
    }
}

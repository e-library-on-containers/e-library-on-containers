using System.Data.Common;
using CSharpFunctionalExtensions;
using Dapper;
using eLibraryOnContainers.Identity.Application.Repositories;
using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using eLibraryOnContainers.Identity.Infrastructure.Common;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public UsersRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    private static async Task<Result<TResult, ApplicationError>> CatchDbException<TIn, TResult>(
        Func<TIn, Task<Result<TResult, ApplicationError>>> func, TIn arg)
    {
        try
        {
            return await func(arg);
        }
        catch (DbException e)
        {
            return Result.Failure<TResult, ApplicationError>(new UnknownError(e.Message));
        }
    }


    public Task<Result<User, ApplicationError>> GetUserByEmailAsync(Email email)
    {
        return CatchDbException(async x =>
        {
            await using var connection = await _connectionFactory.CreateAsync();
            var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email;",
                new { Email = x.Address });
            return Result.SuccessIf<User, ApplicationError>(user != null, user!,
                new NotFoundError("User not found."));
        }, email);
    }

    public Task<Result<bool, ApplicationError>> UpdatePasswordAsync(User user)
    {
        return CatchDbException(async x =>
        {
            await using var connection = await _connectionFactory.CreateAsync();
            var affectedRows = await connection.ExecuteAsync(@"
                UPDATE Users
                SET Password = @Password
                WHERE Id = @Id;",
                new
                {
                    Password = x.Password.Value,
                    x.Id
                });

            return Result.SuccessIf<bool, ApplicationError>(affectedRows == 1, true,
                new NotFoundError("User not found."));
        }, user);
    }

    public Task<Result<bool, ApplicationError>> RegisterUserAsync(User user)
    {
        return CatchDbException(async x =>
        {
            await using var connection = await _connectionFactory.CreateAsync();
            var affectedRows = await connection.ExecuteAsync(@"
                INSERT INTO Users VALUES (@Id, @Email, @Password, @FirstName, @LastName);",
                new
                {
                    x.Id,
                    Email = x.Email.Address,
                    Password = x.Password.Value,
                    x.FullName.FirstName,
                    x.FullName.LastName
                });

            return Result.SuccessIf<bool, ApplicationError>(affectedRows == 1, true,
                new NotFoundError("User not found."));
        }, user);
    }
}
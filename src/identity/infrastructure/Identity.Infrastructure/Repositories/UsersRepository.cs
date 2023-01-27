using System.Data.Common;
using CSharpFunctionalExtensions;
using Dapper;
using FunctionalValidation.Errors;
using Identity.Application.Repositories;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Common;
using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Repositories;

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
            var result = await connection.QueryAsync<ReadUser, ReadRole, ReadUser>(@"
                SELECT 
                    u.id, u.email, u.password, u.first_name as firstName, u.last_name as lastName, ur.user_id as userId, ur.role_name as roleName
                FROM users u
                LEFT JOIN user_roles ur
                ON u.id = ur.user_id
                WHERE email = @Email;",
                (readUser, readRole) =>
                {
                    readUser.Roles.Add(readRole);
                    return readUser;
                },
                new { Email = x.Address },
                splitOn: "userId");

            var user = result.FirstOrDefault();

            return Result
                .SuccessIf<ReadUser, ApplicationError>(user != null, user!, new NotFoundError("User not found."))
                .Bind(u => Email.From(u.Email)
                    .Map(mail => (u, mail)))
                .Bind(t => HashedPassword.FromHashedPassword(t.u.Password)
                    .Map(password => (t.u, t.mail, password)))
                .Bind(t => FullName.From(t.u.FirstName, t.u.LastName)
                    .Map(fullName => (t.u, t.mail, t.password, fullName)))
                .Bind(t => t.u.Roles.Select(x => RoleName.From(x.RoleName))
                    .Combine(errors => errors.First())
                    .Map(roleNames => (t.u, t.mail, t.password, t.fullName, roleNames)))
                .Bind(t => User.From(t.u.Id, t.mail, t.fullName, t.password, t.roleNames.ToArray()));
        }, email);
    }

    public Task<Result<bool, ApplicationError>> UpdatePasswordAsync(User user)
    {
        return CatchDbException(async x =>
        {
            await using var connection = await _connectionFactory.CreateAsync();
            var affectedRows = await connection.ExecuteAsync(@"
                UPDATE users
                SET password = @Password
                WHERE id = @Id;",
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
            await using (var transaction = await connection.BeginTransactionAsync())
            {
                var usersAffectedRows = await connection.ExecuteAsync(@"
                INSERT INTO users VALUES (@Id, @Email, @Password, @FirstName, @LastName);",
                    new
                    {
                        x.Id,
                        Email = x.Email.Address,
                        Password = x.Password.Value,
                        x.FullName.FirstName,
                        x.FullName.LastName
                    },
                    transaction);

                var rolesAffectedRows = await connection.ExecuteAsync(@"
                INSERT INTO user_roles VALUES (@UserId, @RoleName);",
                    x.Roles.Select(rn => new
                    {
                        UserId = x.Id,
                        RoleName = rn.Value
                    }),
                    transaction);

                return await Result.SuccessIf<bool, ApplicationError>(
                        usersAffectedRows == 1 && rolesAffectedRows == x.Roles.Count,
                        true, new UnknownError("Could not register user."))
                    .Tap(() => transaction.CommitAsync());
            }
        }, user);
    }
}
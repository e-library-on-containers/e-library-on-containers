using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;

namespace Identity.Application.Repositories;

public interface IUsersRepository
{
    Task<Result<User, ApplicationError>> GetUserByEmailAsync(Email email);
    Task<Result<bool, ApplicationError>> UpdatePasswordAsync(User user);
    Task<Result<bool, ApplicationError>> RegisterUserAsync(User user);
}
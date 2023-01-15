using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Application.Repositories;

public interface IUsersRepository
{
    Task<Result<User, ApplicationError>> GetUserByEmailAsync(Email email);
    Task<Result<bool, ApplicationError>> UpdatePasswordAsync(User user);
    Task<Result<bool, ApplicationError>> RegisterUserAsync(User user);
}
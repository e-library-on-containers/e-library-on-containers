using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Entities;

namespace Identity.Application.Services;

public interface IRolesService
{
    Task<Result<Role, ApplicationError>> GetDefaultRoleAsync();
}
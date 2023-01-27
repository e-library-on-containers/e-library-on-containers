using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;

namespace Identity.Infrastructure.Services;

public class RolesService : IRolesService
{
    public Task<Result<Role, ApplicationError>> GetDefaultRoleAsync()
    {
        return Task.FromResult(RoleName.From("User").Map(n => new Role(Guid.NewGuid(), n)));
    }
}
using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Services;
using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Infrastructure.Services;

public class RolesService : IRolesService
{
    public Task<Result<Role, ApplicationError>> GetDefaultRoleAsync()
    {
        return Task.FromResult(RoleName.From("User").Map(n => new Role(Guid.NewGuid(), n)));
    }
}
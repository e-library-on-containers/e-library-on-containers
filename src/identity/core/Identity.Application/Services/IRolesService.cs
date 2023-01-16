using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Entities;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Application.Services;

public interface IRolesService
{
    Task<Result<Role, ApplicationError>> GetDefaultRoleAsync();
}
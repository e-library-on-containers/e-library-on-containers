using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Dtos;
using eLibraryOnContainers.Identity.Domain.Entities;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Application.Services;

public interface IAuthService
{
    Result<TokenDto, ApplicationError> SingIn(User user);
}
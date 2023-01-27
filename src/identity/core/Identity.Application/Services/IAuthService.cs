using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Application.Dtos;
using Identity.Domain.Entities;

namespace Identity.Application.Services;

public interface IAuthService
{
    Result<TokenDto, ApplicationError> SingIn(User user);
}
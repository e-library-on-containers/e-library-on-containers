using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Users;
using FunctionalValidation;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;

namespace eLibraryOnContainers.Identity.Api.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public override Result<RegisterUserCommand, ApplicationError> Validate(Result<RegisterUserCommand, ApplicationError> result)
    {
        return result
            .Ensure(q => !string.IsNullOrWhiteSpace(q.Email), "E-mail address must not be empty.")
            .Ensure(q => !string.IsNullOrWhiteSpace(q.Password), "Password must not be empty.")
            .Ensure(q => !string.IsNullOrWhiteSpace(q.FirstName), "First name must not be empty.")
            .Ensure(q => !string.IsNullOrWhiteSpace(q.LastName), "Last name must not be empty.");
    }
}
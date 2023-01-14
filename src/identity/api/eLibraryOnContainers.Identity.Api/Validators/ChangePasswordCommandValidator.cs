using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Users;
using FunctionalValidation;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;

namespace eLibraryOnContainers.Identity.Api.Validators;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public override Result<ChangePasswordCommand, ApplicationError> Validate(Result<ChangePasswordCommand, ApplicationError> result)
    {
        return result
            .Ensure(q => !string.IsNullOrWhiteSpace(q.Email), "E-mail address must not be empty!")
            .Ensure(q => !string.IsNullOrWhiteSpace(q.OldPassword), "Current password must not be empty!")
            .Ensure(q => !string.IsNullOrWhiteSpace(q.NewPassword), "New password must not be empty!");
    }
}
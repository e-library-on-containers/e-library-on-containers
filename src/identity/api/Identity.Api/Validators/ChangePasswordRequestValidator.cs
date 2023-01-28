using CSharpFunctionalExtensions;
using FunctionalValidation;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;
using Identity.Api.Request;

namespace Identity.Api.Validators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public override Result<ChangePasswordRequest, ApplicationError> Validate(Result<ChangePasswordRequest, ApplicationError> result)
    {
        return result
            .Ensure(q => !string.IsNullOrWhiteSpace(q.OldPassword), "Current password must not be empty.")
            .Ensure(q => !string.IsNullOrWhiteSpace(q.NewPassword), "New password must not be empty.");
    }
}
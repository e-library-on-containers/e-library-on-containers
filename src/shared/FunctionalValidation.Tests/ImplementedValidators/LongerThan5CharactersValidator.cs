using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;

namespace FunctionalValidation.Tests.ImplementedValidators;

internal class LongerThan5CharactersValidator : AbstractValidator<string>
{
    public const string ErrorMessage = "String is not longer than 5 characters.";
    public override Result<string, ApplicationError> Validate(Result<string, ApplicationError> result) =>
        result.Ensure(s => s.Length > 5, ErrorMessage);
}
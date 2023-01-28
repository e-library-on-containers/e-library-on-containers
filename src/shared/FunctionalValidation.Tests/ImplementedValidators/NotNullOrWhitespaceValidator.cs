using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;

namespace FunctionalValidation.Tests.ImplementedValidators;

internal class NotNullOrWhitespaceValidator : AbstractValidator<string>
{
    public const string ErrorMessage = "String is null or whitespace.";
    public override Result<string, ApplicationError> Validate(Result<string, ApplicationError> result) =>
        result.Ensure(s => !string.IsNullOrWhiteSpace(s), ErrorMessage);
}
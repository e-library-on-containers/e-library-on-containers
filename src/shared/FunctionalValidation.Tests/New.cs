using FunctionalValidation.Tests.ImplementedValidators;

namespace FunctionalValidation.Tests;

internal static class New
{
    internal static AbstractValidator<string> NotNullOrWhitespaceStringValidator() => new NotNullOrWhitespaceValidator();
    internal static string NotNullOrWhitespaceStringValidatorErrorMessage => NotNullOrWhitespaceValidator.ErrorMessage;
    
    internal static AbstractValidator<string> LongerThan5CharactersStringValidator() => new LongerThan5CharactersValidator();
    internal static string LongerThan5CharactersStringValidatorErrorMessage => LongerThan5CharactersValidator.ErrorMessage;
}
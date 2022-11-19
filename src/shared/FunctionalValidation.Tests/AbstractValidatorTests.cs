using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace FunctionalValidation.Tests;

[Subject(typeof(AbstractValidator<>))]
internal class AbstractValidatorTests
{
    static AbstractValidator<string> sut;
    static Result<string, ApplicationError> result;
    static string value;

    Establish ctx = () => sut = New.NotNullOrWhitespaceStringValidator();

    Because of = () => result = sut.Validate(value);

    class When_predicate_failed
    {
        const string errorMessage = "String is null or whitespace.";
        Establish ctx = () => value = string.Empty;

        It should_fail = () => result.IsFailure.ShouldBeTrue();

        It should_have_ValidationError = () => result.Error.ShouldBeOfExactType<ValidationError>();

        It should_have_error_with_correct_message = () => result.Error.Message.ShouldEqual(errorMessage);
    }

    class When_predicate_succeeded
    {
        Establish ctx = () => value = "Valid data";

        It should_succeed = () => result.IsSuccess.ShouldBeTrue();

        It should_have_value_set = () => result.Value.ShouldEqual(value);
    }
}
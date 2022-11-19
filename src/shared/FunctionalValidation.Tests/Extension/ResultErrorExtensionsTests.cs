using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;
using Machine.Specifications;
using Result = CSharpFunctionalExtensions.Result;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace FunctionalValidation.Tests.Extension;

[Subject(typeof(ResultErrorExtensions))]
internal class ResultErrorExtensionsTests
{
    static Result<string, ApplicationError> result;
    static Result<string, ApplicationError> value;
    static Func<string, bool> predicate;
    static string message;

    Establish ctx = () =>
    {
        message = "String is null or whitespace.";
        predicate = s => !string.IsNullOrWhiteSpace(s);
    };
    
    Because of = () => result = value.Ensure(predicate, message);

    class When_predicate_failed
    {
        Establish ctx = () => value = Result.Success<string, ApplicationError>(string.Empty);

        It should_fail = () => result.IsFailure.ShouldBeTrue();

        It should_have_ValidationError = () => result.Error.ShouldBeOfExactType<ValidationError>();

        It should_have_error_with_correct_message = () => result.Error.Message.ShouldEqual(message);
    }

    class When_predicate_succeeded
    {
        const string data = "Valid data";

        Establish ctx = () => value = Result.Success<string, ApplicationError>(data);

        It should_succeed = () => result.IsSuccess.ShouldBeTrue();

        It should_have_value_set = () => result.Value.ShouldEqual(data);
    }
}
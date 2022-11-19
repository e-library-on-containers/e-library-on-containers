using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace FunctionalValidation.Tests;

[Subject(typeof(FunctionalValidator))]
internal class FunctionalValidatorTests : WithSubject<FunctionalValidator>
{
    static string value;
    static Result<string, ApplicationError> result;

    Establish ctx = () =>
    {
        var services = new ServiceCollection();
        services.AddSingleton<AbstractValidator<string>>(New.NotNullOrWhitespaceStringValidator());
        services.AddSingleton<AbstractValidator<string>>(New.LongerThan5CharactersStringValidator());
        
        Configure(x => x.For<IServiceProvider>().Use(services.BuildServiceProvider));
    };

    Because of = () => result = Subject.Validate(value);
    

    class When_first_validator_fails
    {
        Establish ctx = () => value = string.Empty;

        It should_return_first_validator_error_message = () =>
            result.Error.Message.ShouldEqual(New.NotNullOrWhitespaceStringValidatorErrorMessage);
     
        It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();
    }

    class When_second_validator_fails
    {
        Establish ctx = () => value = "12345";

        It should_return_second_validator_error_message = () =>
            result.Error.Message.ShouldEqual(New.LongerThan5CharactersStringValidatorErrorMessage);

        It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();
    }

    class When_no_validators_fail
    {
        Establish ctx = () => value = "123456";

        It should_return_success_result = () => result.IsSuccess.ShouldBeTrue();
    }
}
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Errors;
using Identity.Domain.ValueObjects;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace Identity.Tests.Core.Domain.ValueObjects;

[Subject(typeof(Email))]
public class EmailTests
{
    static string address;
    static Result<Email, ApplicationError> result;

    Because of = () => result = Email.From(address);

    class When_valid_email
    {
        Establish ctx = () => address = "mail@domain.com";

        It should_return_success_result = () => result.IsSuccess.ShouldBeTrue();
    }

    class When_invalid_email
    {
        class When_null_address
        {
            Establish ctx = () => address = null;

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_email_error = () => result.Error.ShouldBeOfExactType<InvalidEmail>();
        }

        class When_empty_address
        {
            Establish ctx = () => address = string.Empty;

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_email_error = () => result.Error.ShouldBeOfExactType<InvalidEmail>();
        }

        class When_whitespace_address
        {
            Establish ctx = () => address = " ";

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_email_error = () => result.Error.ShouldBeOfExactType<InvalidEmail>();
        }

        class When_no_name
        {
            Establish ctx = () => address = "@domain.com";

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_email_error = () => result.Error.ShouldBeOfExactType<InvalidEmail>();
        }

        class When_no_domain
        {
            Establish ctx = () => address = "mail@";

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_email_error = () => result.Error.ShouldBeOfExactType<InvalidEmail>();
        }

        class When_no_at_symbol
        {
            Establish ctx = () => address = "maildomain.com";

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_email_error = () => result.Error.ShouldBeOfExactType<InvalidEmail>();
        }
    }
}
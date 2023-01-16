using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace eLibraryOnContainers.Identity.Tests.Core.Domain.ValueObjects;

[Subject(typeof(FullName))]
public class FullNameTests
{
    static string firstName;
    static string lastName;
    static Result<FullName, ApplicationError> result;

    Because of = () => result = FullName.From(firstName, lastName);

    class When_valid_full_name
    {
        Establish ctx = () => (firstName, lastName) = ("validFirstName", "validLastName");

        It should_return_success_result = () => result.IsSuccess.ShouldBeTrue();
    }

    class When_invalid_full_name
    {
        class When_invalid_first_name
        {
            Establish ctx = () => lastName = "validLastName";

            class When_null
            {
                Establish ctx = () => firstName = null;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_empty
            {
                Establish ctx = () => firstName = string.Empty;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_whitespace
            {
                Establish ctx = () => firstName = " ";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }
        }

        class When_invalid_last_name
        {
            Establish ctx = () => firstName = "validFirstName";

            class When_null
            {
                Establish ctx = () => lastName = null;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_empty
            {
                Establish ctx = () => lastName = string.Empty;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_whitespace
            {
                Establish ctx = () => lastName = " ";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }
        }

        class When_both_invalid
        {
            class When_both_nulls
            {
                Establish ctx = () => (firstName, lastName) = (null, null);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_null_and_empty
            {
                Establish ctx = () => (firstName, lastName) = (null, string.Empty);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_null_and_whitespace
            {
                Establish ctx = () => (firstName, lastName) = (null, " ");

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_both_empty
            {
                Establish ctx = () => (firstName, lastName) = (string.Empty, string.Empty);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_empty_null
            {
                Establish ctx = () => (firstName, lastName) = (string.Empty, null);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_empty_whitespace
            {
                Establish ctx = () => (firstName, lastName) = (string.Empty, " ");

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_both_whitespace
            {
                Establish ctx = () => (firstName, lastName) = (" ", " ");

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_whitespace_null
            {
                Establish ctx = () => (firstName, lastName) = (" ", null);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }

            class When_whitespace_empty
            {
                Establish ctx = () => (firstName, lastName) = (" ", string.Empty);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_full_name_error = () => result.Error.ShouldBeOfExactType<InvalidFullName>();
            }
        }
    }
}
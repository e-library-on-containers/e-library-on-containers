using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Errors;
using Identity.Domain.ValueObjects;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace Identity.Tests.Core.Domain.ValueObjects;

[Subject(typeof(HashedPassword))]
public class HashedPasswordTests
{
    static Result<HashedPassword, ApplicationError> result;

    class When_hashed_password
    {
        static string hashedPassword;

        Because of = () => result = HashedPassword.FromHashedPassword(hashedPassword);

        class When_valid
        {
            Establish ctx = () => hashedPassword = Functions.HashPassword("Password1234");

            It should_return_success_result = () => result.IsSuccess.ShouldBeTrue();
        }

        class When_invalid
        {
            class When_null
            {
                Establish ctx = () => hashedPassword = null;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }

            class When_empty
            {
                Establish ctx = () => hashedPassword = string.Empty;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }

            class When_whitespace
            {
                Establish ctx = () => hashedPassword = " ";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }
        }
    }

    class When_raw_password
    {
        static string rawPassword;

        Because of = () => result = HashedPassword.FromRawPassword(rawPassword, Functions.HashPassword);

        class When_valid
        {
            Establish ctx = () => rawPassword = "Password1234";

            It should_return_success_result = () => result.IsSuccess.ShouldBeTrue();
        }

        class When_invalid
        {
            class When_null
            {
                Establish ctx = () => rawPassword = null;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }

            class When_empty
            {
                Establish ctx = () => rawPassword = string.Empty;

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }

            class When_whitespace
            {
                Establish ctx = () => rawPassword = " ";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }

            class When_longer_than_max_length
            {
                Establish ctx = () => rawPassword = new string('p', HashedPassword.MAX_LENGTH + 1);

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_invalid_password_error = () => result.Error.ShouldBeOfExactType<InvalidPassword>();
            }
        }

        class When_unsafe
        {
            class When_no_numbers
            {
                Establish ctx = () => rawPassword = "passwordtest";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_unsafe_password_error = () => result.Error.ShouldBeOfExactType<UnsafePassword>();
            }

            class When_no_letters
            {
                Establish ctx = () => rawPassword = "1234567890";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_unsafe_password_error = () => result.Error.ShouldBeOfExactType<UnsafePassword>();
            }

            class When_no_lowercase_letter
            {
                Establish ctx = () => rawPassword = "PASSWORD1234";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_unsafe_password_error = () => result.Error.ShouldBeOfExactType<UnsafePassword>();
            }

            class When_no_uppercase_letter
            {
                Establish ctx = () => rawPassword = "password1234";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_unsafe_password_error = () => result.Error.ShouldBeOfExactType<UnsafePassword>();
            }

            class When_shorter_than_min_length
            {
                Establish ctx = () => rawPassword = "ps";

                It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

                It should_return_unsafe_password_error = () => result.Error.ShouldBeOfExactType<UnsafePassword>();
            }
        }
    }
}
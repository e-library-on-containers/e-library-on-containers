using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace eLibraryOnContainers.Identity.Tests.Core.Domain.ValueObjects;

[Subject(typeof(RoleName))]
public class RoleNameTests
{
    static string roleName;
    static Result<RoleName, ApplicationError> result;

    Because of = () => result = RoleName.From(roleName);

    class When_valid_role_name
    {
        Establish ctx = () => roleName = "validRoleName";

        It should_return_success_result = () => result.IsSuccess.ShouldBeTrue();
    }

    class When_invalid_role_name
    {
        class When_null
        {
            Establish ctx = () => roleName = null;

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_role_name_error = () => result.Error.ShouldBeOfExactType<InvalidRoleName>();
        }

        class When_empty
        {
            Establish ctx = () => roleName = string.Empty;

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_role_name_error = () => result.Error.ShouldBeOfExactType<InvalidRoleName>();
        }

        class When_whitespace
        {
            Establish ctx = () => roleName = " ";

            It should_return_failed_result = () => result.IsFailure.ShouldBeTrue();

            It should_return_invalid_role_name_error = () => result.Error.ShouldBeOfExactType<InvalidRoleName>();
        }
    }
}
using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace eLibraryOnContainers.Identity.Tests.Core.Domain.Entities;

[Subject(typeof(User))]
public class UserTests
{
    static Result<User, ApplicationError> CreateUser(string email, string firstName, string lastName, string password, RoleName[] roleNames) =>
        User.From(Guid.NewGuid(), Email.From(email).Value,
            FullName.From(firstName, lastName).Value,
            New.HashedPassword(password),
            roleNames);

    static void UsersShouldMatch(Result<User, ApplicationError> actual, User expected) =>
        actual.Value.ShouldMatch(x =>
            x.Email.Equals(expected.Email) && x.FullName.Equals(expected.FullName) &&
            x.Password.Equals(expected.Password) && x.Roles.Equals(expected.Roles));

    const string email = "mail@domain.com";
    const string firstName = "First";
    const string lastName = "Last";
    const string rawPassword = "Password1234";


    class When_user_roles_valid
    {
        static User user;
        static HashedPassword oldPasswordCandidate;
        static HashedPassword newPassword;
        const string newRawPassword = "NewPassword1234";
        static Result<User, ApplicationError> updatedUser;
        static RoleName[] roleNames;

        Establish ctx = () =>
        {
            roleNames = new[] { New.RoleName("User") };
            user = CreateUser(email, firstName, lastName, rawPassword, roleNames).Value;
            newPassword = New.HashedPassword(newRawPassword);
        };

        Because of = () => updatedUser = user.WithNewPassword(oldPasswordCandidate, newPassword);

        class When_passwords_does_not_match
        {
            Establish ctx = () => oldPasswordCandidate = New.HashedPassword("Password123");

            It should_return_failed_result = () => updatedUser.IsFailure.ShouldBeTrue();

            It should_return_passwords_does_not_match_error = () => updatedUser.Error.ShouldBeOfExactType<PasswordsDoesNotMatch>();
        }

        class When_passwords_match
        {
            static User expectedUser;

            Establish ctx = () =>
            {
                oldPasswordCandidate = New.HashedPassword("Password1234"); 
                expectedUser = CreateUser(email, firstName, lastName, newRawPassword, roleNames).Value;
            };

            It should_return_success_result = () => updatedUser.IsSuccess.ShouldBeTrue();

            It should_return_new_user_with_updated_password = () => UsersShouldMatch(updatedUser, expectedUser);
        }
    }

    class When_user_roles_invalid
    {
        static Result<User, ApplicationError> user;
        
        Establish ctx = () => user = CreateUser(email, firstName, lastName, rawPassword, Array.Empty<RoleName>());

        It should_return_failed_result = () => user.IsFailure.ShouldBeTrue();

        It should_return_no_user_roles_error = () => user.Error.ShouldBeOfExactType<NoUserRoles>();
    }
}
using Machine.Fakes;
using Machine.Specifications;
using CSharpFunctionalExtensions;
using DotNet.Testcontainers.Containers;
using FunctionalValidation.Errors;
using Microsoft.Extensions.Options;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using Identity.Infrastructure.Common;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Repositories;
using Identity.IntegrationTests.Helpers;
using Identity.Tests;

namespace Identity.IntegrationTests.Infrastructure;

[Subject(typeof(UsersRepository))]
public class UsersRepositoryTests : WithSubject<UsersRepository>
{
    private static PostgreSqlTestcontainer container;

    private Establish ctx = async () =>
    {
        container = await DatabaseSetupHelpers.StartContainerAsync();

        Configure(x => x
            .For<ISqlConnectionFactory>()
            .Use(() =>
                new NpgsqlConnectionFactory(Options.Create(
                    new SqlOptions { ConnectionString = container.ConnectionString }))));

    };


    class When_user_with_email_not_exist
    {
        private static User user;
        private const string address = "other@domain.com";
        private static readonly Guid id = Guid.NewGuid();
        private static Email email;

        private Establish ctx = () => email = Email.From(address).Value;

        class When_getting_user
        {
            private static Result<User, ApplicationError> result;
            private Because of = async () => result = await Subject.GetUserByEmailAsync(email);

            private It should_return_failure = () =>
            {
                result.IsFailure.ShouldBeTrue();
            };
            private It should_return_not_found_error = () =>
            {
                result.Error.ShouldBeOfExactType<NotFoundError>();
            };
        }

        class When_updating_password
        {
            private static Result<bool, ApplicationError> result;

            private Establish ctx = () => user = New.User(id, address);

            private Because of = async () => result = await Subject.UpdatePasswordAsync(user);

            private It should_return_failure = () => result.IsFailure.ShouldBeTrue();
            private It should_return_not_found_error = () => result.Error.ShouldBeOfExactType<NotFoundError>();
        }

        class When_registering_user
        {
            private static Result<bool, ApplicationError> result;

            private Establish ctx = () => user = New.User(email: address);

            private Because of = async () => result = await Subject.RegisterUserAsync(user);

            private It should_return_success = () => result.IsSuccess.ShouldBeTrue();

            private It should_save_user_in_database = async () =>
            {
                var savedUser = await Subject.GetUserByEmailAsync(email);
                savedUser.Value.ShouldEqual(user);
            };
        }
    }

    class When_user_with_email_exists
    {
        private const string address = "admin@domain.com";
        private static Email email;

        private Establish ctx = () => email = Email.From(address).Value;


    }

    private Cleanup after = async () => await container.StopAsync();
}
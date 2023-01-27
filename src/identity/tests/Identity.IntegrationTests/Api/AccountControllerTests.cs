using System.Net;
using System.Net.Http.Json;
using DotNet.Testcontainers.Containers;
using Identity.Api.Controllers;
using Identity.Api.Request;
using Identity.Api.Responses;
using Identity.Application.Authentication;
using Identity.Application.Users;
using Identity.IntegrationTests.Configuration;
using Identity.IntegrationTests.Helpers;
using Machine.Specifications;
using Newtonsoft.Json.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace Identity.IntegrationTests.Api;

[Subject(typeof(AccountController))]
public class AccountControllerTests
{
    static PostgreSqlTestcontainer container;
    static TestWebAppFactory webAppFactory;
    static IdentityClient identityClient;
    static HttpResponseMessage result;
    static HttpClient client;

    static string email;
    static string password;

    Establish ctx = async () =>
    {
        container = await DatabaseSetupHelpers.StartContainerAsync();

        webAppFactory = new TestWebAppFactory(container.ConnectionString);
        client = webAppFactory.CreateClient();
        identityClient = new IdentityClient(client);
    };

    class When_user_not_exist
    {
        static string firstName;
        static string lastName;

        Because of = async () =>
            result = await identityClient.RegisterAsync(new RegisterUserCommand(email, firstName, lastName, password));

        class When_email_empty
        {
            Establish ctx = () =>
            {
                email = string.Empty;
                password = "Password1234";
                firstName = "First";
                lastName = "Last";
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_empty_email_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("E-mail address must not be empty."));
        }

        class When_password_empty
        {
            Establish ctx = () =>
            {
                email = "user@domain.com";
                password = string.Empty;
                firstName = "First";
                lastName = "Last";
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_empty_password_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("Password must not be empty."));
        }

        class When_password_unsafe
        {
            Establish ctx = () =>
            {
                email = "user@domain.com";
                password = "Password";
                firstName = "First";
                lastName = "Last";
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_unsafe_password_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("Password is not safe enough."));
        }

        class When_first_name_empty
        {
            Establish ctx = () =>
            {
                email = "user@domain.com";
                password = "Password1234";
                firstName = string.Empty;
                lastName = "Last";
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_empty_first_name_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("First name must not be empty."));
        }

        class When_last_name_empty
        {
            Establish ctx = () =>
            {
                email = "user@domain.com";
                password = "Password1234";
                firstName = "First";
                lastName = string.Empty;
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_empty_last_name_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("Last name must not be empty."));
        }

        class When_all_fields_empty
        {
            Establish ctx = () =>
            {
                email = string.Empty;
                password = string.Empty;
                firstName = string.Empty;
                lastName = string.Empty;
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_empty_email_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("E-mail address must not be empty."));
        }

        class When_all_fields_valid
        {
            Establish ctx = () =>
            {
                email = "user@domain.com";
                password = "Password1234";
                firstName = "First";
                lastName = "Last";
            };

            It should_return_created = () => result.StatusCode.ShouldEqual(HttpStatusCode.Created);

            It should_allow_user_to_sign_in = async () =>
                (await identityClient.GetAuthTokenAsync(new SignInQuery(email, password))).ShouldNotBeNull();
        }
    }

    class When_user_exists
    {
        static string oldPassword;
        static string newPassword;

        Establish ctx = () => email = "admin@domain.com";

        class When_authenticated
        {
            static string token;

            Establish ctx = async () =>
            {
                password = "Password1234";
                token = await identityClient.GetAuthTokenAsync(new SignInQuery(email, password));
            };

            Because of = async () =>
                result = await identityClient.ChangePasswordAsync(
                    new ChangePasswordRequest
                    {
                        OldPassword = oldPassword,
                        NewPassword = newPassword
                    }, token);

            class When_old_password_empty
            {
                Establish ctx = () =>
                {
                    oldPassword = string.Empty;
                    newPassword = "Password12345";
                };

                It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

                It should_return_empty_old_password_message = async () =>
                    (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                        new ErrorMessage("Current password must not be empty."));
            }

            class When_old_password_incorrect
            {
                Establish ctx = () =>
                {
                    oldPassword = "Password123";
                    newPassword = "Password12345";
                };

                It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

                It should_return_passwords_does_not_match_message = async () =>
                    (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                        new ErrorMessage("Passwords does not match."));
            }

            class When_new_password_empty
            {
                Establish ctx = () =>
                {
                    oldPassword = "Password1234";
                    newPassword = string.Empty;
                };

                It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

                It should_return_empty_new_password_message = async () =>
                    (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                        new ErrorMessage("New password must not be empty."));
            }

            class When_new_password_unsafe
            {
                Establish ctx = () =>
                {
                    oldPassword = "Password1234";
                    newPassword = "Password";
                };

                It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

                It should_return_password_not_safe_enough_message = async () =>
                    (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                        new ErrorMessage("Password is not safe enough."));
            }

            class When_all_fields_empty
            {
                Establish ctx = () =>
                {
                    oldPassword = string.Empty;
                    newPassword = string.Empty;
                };

                It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

                It should_return_empty_old_password_message = async () =>
                    (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                        new ErrorMessage("Current password must not be empty."));
            }

            class When_all_fields_valid
            {
                Establish ctx = () =>
                {
                    oldPassword = "Password1234";
                    newPassword = "Password12345";
                };

                It should_return_accepted = () => result.StatusCode.ShouldEqual(HttpStatusCode.Accepted);

                It should_invalidate_old_password = async () =>
                    (await identityClient.SignInAsync(new SignInQuery(email, oldPassword))).StatusCode.ShouldEqual(
                        HttpStatusCode.BadRequest);

                It should_allow_using_new_password = async () =>
                    (await identityClient.SignInAsync(new SignInQuery(email, newPassword))).StatusCode.ShouldEqual(
                        HttpStatusCode.OK);

                It should_return_token_using_new_password = async () =>
                    (await identityClient.GetAuthTokenAsync(new SignInQuery(email, newPassword))).ShouldNotBeNull();
            }
        }

        class When_not_authenticated
        {
            Because of = async () =>
                result = await identityClient.ChangePasswordAsync(
                    new ChangePasswordRequest
                    {
                        OldPassword = oldPassword,
                        NewPassword = newPassword
                    });

            It should_return_unauthorized = () => result.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
        }

        class When_using_same_email
        {
            static string firstName;
            static string lastName;

            Establish ctx = () =>
            {
                password = "DifferentPassword1234";
                firstName = "First";
                lastName = "Last";
            };

            Because of = async () =>
                result = await identityClient.RegisterAsync(new RegisterUserCommand(email, firstName, lastName,
                    password));

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_user_already_exists_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("User with this e-mail address already exists."));

        }
    }

    Cleanup after = async () =>
    {
        await container.StopAsync();
        client.Dispose();
    };
}
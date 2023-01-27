using Machine.Specifications;
using System.Net;
using System.Net.Http.Json;
using DotNet.Testcontainers.Containers;
using Identity.Api.Controllers;
using Identity.Api.Responses;
using Identity.Application.Authentication;
using Identity.Application.Dtos;
using Identity.IntegrationTests.Helpers;
using Identity.IntegrationTests.Configuration;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace Identity.IntegrationTests.Api;

[Subject(typeof(AuthController))]
public class AuthControllerTests
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

    Because of = async () => result = await identityClient.SignInAsync(new SignInQuery(email, password));

    class When_email_is_empty
    {
        Establish ctx = () =>
        {
            email = string.Empty;
            password = "Password1234";
        };

        It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        It should_return_invalid_email_message = async () =>
            (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                new ErrorMessage("E-mail address must not be empty."));
    }

    class When_password_is_empty
    {
        Establish ctx = () =>
        {
            email = "admin@domain.com";
            password = string.Empty;
        };

        It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        It should_return_invalid_password_message = async () =>
            (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                new ErrorMessage("Password must not be empty."));
    }

    class When_email_and_password_empty
    {
        Establish ctx = () =>
        {
            email = string.Empty;
            password = string.Empty;
        };

        It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        It should_return_invalid_email_message = async () =>
            (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                new ErrorMessage("E-mail address must not be empty."));
    }

    class When_user_with_email_not_exist
    {
        Establish ctx = () =>
        {
            email = "other@domain.com";
            password = "Password1234";
        };

        It should_return_not_found = () => result.StatusCode.ShouldEqual(HttpStatusCode.NotFound);

        It should_return_user_not_found_message = async () =>
            (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                new ErrorMessage("User not found."));
    }

    class When_user_with_email_exists
    {
        class When_incorrect_password
        {
            Establish ctx = () =>
            {
                email = "admin@domain.com";
                password = "P@ssword1234";
            };

            It should_return_bad_request = () => result.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

            It should_return_password_does_not_match_message = async () =>
                (await result.Content.ReadFromJsonAsync<ErrorMessage>()).ShouldBeLike(
                    new ErrorMessage("Passwords does not match."));
        }

        class When_correct_password
        {
            Establish ctx = () =>
            {
                email = "admin@domain.com";
                password = "Password1234";
            };

            It should_return_ok = () => result.StatusCode.ShouldEqual(HttpStatusCode.OK);

            It should_return_token = async () =>
                (await result.Content.ReadFromJsonAsync<TokenDto>())!.Token.ShouldNotBeNull();
        }
    }

    Cleanup after = async () =>
    {
        await container.StopAsync();
        client.Dispose();
    };

}
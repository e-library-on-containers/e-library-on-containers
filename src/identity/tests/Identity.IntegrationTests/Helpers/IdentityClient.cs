using System.Net.Http.Json;
using Identity.Api.Request;
using Identity.Application.Authentication;
using Identity.Application.Dtos;
using Identity.Application.Users;

namespace Identity.IntegrationTests.Helpers;

public class IdentityClient
{
    private readonly HttpClient _client;

    public IdentityClient(HttpClient client)
    {
        _client = client;
    }

    public Task<HttpResponseMessage> SignInAsync(SignInQuery query) =>
        _client.PostAsJsonAsync("auth/authenticate", query);

    public Task<HttpResponseMessage> RegisterAsync(RegisterUserCommand command) =>
        _client.PostAsJsonAsync("account/register", command);

    public async Task<string> GetAuthTokenAsync(SignInQuery query)
    {
        var response = await SignInAsync(query);
        return (await response.Content.ReadFromJsonAsync<TokenDto>())!.Token;
    }

    public Task<HttpResponseMessage> ChangePasswordAsync(ChangePasswordRequest command, string token = null)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(command),
            Method = HttpMethod.Post,
            RequestUri = new Uri("account/changePassword", UriKind.Relative)
        };

        if (token != null)
        {
            request.Headers.Add("Authorization", $"Bearer {token}");
        }

        return _client.SendAsync(request);
    }
}
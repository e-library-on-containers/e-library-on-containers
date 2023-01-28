using System.IdentityModel.Tokens.Jwt;

namespace Identity.Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserEmail(this HttpContext httpContext) => httpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;
}
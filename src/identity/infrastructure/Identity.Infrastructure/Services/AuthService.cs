using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Application.Dtos;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IAuthOptions _options;

    public AuthService(IOptions<IAuthOptions> options)
    {
        _options = options.Value;
    }

    public Result<TokenDto, ApplicationError> SingIn(User user)
    {
        var now = DateTimeOffset.UtcNow;
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email.Address),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.FullName.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString()),
        };

        claims.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Value)));

        var expires = now.AddMinutes(_options.ExpiredInMinutes);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key)),
            SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            claims: claims,
            notBefore: now.UtcDateTime,
            expires: expires.UtcDateTime,
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Result.Success<TokenDto, ApplicationError>(new TokenDto(token, expires.ToUnixTimeSeconds().ToString()));
    }
}
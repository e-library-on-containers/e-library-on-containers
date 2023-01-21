namespace eLibraryOnContainers.Identity.Application.Dtos;

public class TokenDto
{
    public string Token { get; }
    public string Expires { get; }

    public TokenDto(string token, string expires)
    {
        Token = token;
        Expires = expires;
    }
}
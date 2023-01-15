using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Dtos;
using eLibraryOnContainers.Identity.Application.Repositories;
using eLibraryOnContainers.Identity.Application.Services;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;
using MediatR;

namespace eLibraryOnContainers.Identity.Application.Authentication;

public class SignInQuery : IRequest<Result<TokenDto, ApplicationError>>
{
    public string Email { get; }
    public string Password { get; }

    public SignInQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class SignInQueryHandler : IRequestHandler<SignInQuery, Result<TokenDto, ApplicationError>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthService _authService;

    public SignInQueryHandler(IUsersRepository usersRepository, IAuthService authService)
    {
        _usersRepository = usersRepository;
        _authService = authService;
    }

    public async Task<Result<TokenDto, ApplicationError>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        return await Email.From(request.Email)
            .Bind(email => _usersRepository.GetUserByEmailAsync(email)
                .Map(user => (user, email)))
            .Bind(t => t.user.Password.Challenge(request.Password, HashedPassword.DefaultHasher)
                .Map(password => (t.user, password)))
            .Map(t => t.user)
            .Bind(user => _authService.SingIn(user));
    }
}
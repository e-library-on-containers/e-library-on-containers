using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Application.Repositories;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Errors;
using Identity.Domain.ValueObjects;
using MediatR;

namespace Identity.Application.Users;

public class RegisterUserCommand : IRequest<Result<Unit, ApplicationError>>
{
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Password { get; }

    public RegisterUserCommand(string email, string firstName, string lastName, string password)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Unit, ApplicationError>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRolesService _rolesService;

    public RegisterUserCommandHandler(IUsersRepository usersRepository, IRolesService rolesService)
    {
        _usersRepository = usersRepository;
        _rolesService = rolesService;
    }

    private static Func<Email, Func<Result<HashedPassword, ApplicationError>, Func<Result<FullName, ApplicationError>,
        Func<Result<Role, ApplicationError>, Func<Guid, Result<User, ApplicationError>>>>>> CurriedUserCreator =>
        email =>
            password =>
                name =>
                    role =>
                        guid =>
                            password.Bind(p =>
                                name.Bind(n =>
                                    role.Bind(r =>
                                        User.From(guid, email, n, p, new[] { r.RoleName }))));

    public async Task<Result<Unit, ApplicationError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await Email.From(request.Email)
            .Ensure(async email => (await _usersRepository.GetUserByEmailAsync(email)).IsFailure,
                new UserAlreadyExists())
            .Map(CurriedUserCreator)
            .Bind(func => HashedPassword.FromRawPassword(request.Password, HashedPassword.DefaultHasher)
                .Map(password => func(password)))
            .Bind(func => FullName.From(request.FirstName, request.LastName)
                .Map(name => func(name)))
            .Bind(async func => await _rolesService.GetDefaultRoleAsync()
                .Map(role => func(role)))
            .Bind(func => func(Guid.NewGuid()))
            .Bind(user => _usersRepository.RegisterUserAsync(user))
            .Map(_ => Unit.Value);
    }
}
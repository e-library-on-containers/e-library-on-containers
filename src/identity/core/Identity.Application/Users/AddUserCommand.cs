using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Application.Repositories;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Errors;
using Identity.Domain.ValueObjects;
using MediatR;

namespace Identity.Application.Users;

public class AddUserCommand : IRequest<Result<Unit, ApplicationError>>
{
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Password { get; }
    
    public string RoleName { get; }

    public AddUserCommand(string email, string firstName, string lastName, string password, string roleName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        RoleName = roleName;
    }
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<Unit, ApplicationError>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRolesService _rolesService;

    public AddUserCommandHandler(IUsersRepository usersRepository, IRolesService rolesService)
    {
        _usersRepository = usersRepository;
        _rolesService = rolesService;
    }

    public async Task<Result<Unit, ApplicationError>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        return await Email.From(request.Email)
            .Ensure(async email => (await _usersRepository.GetUserByEmailAsync(email)).IsFailure, new UserAlreadyExists())
            .Bind(email => HashedPassword.FromRawPassword(request.Password, HashedPassword.DefaultHasher)
                .Map(password => (email, password)))
            .Bind(t => FullName.From(request.FirstName, request.LastName)
                .Map(f => (t.email, t.password, fullName: f)))
            .Bind(t => _rolesService.GetNamedRoleAsync(request.RoleName)
                .Map(r => (t.email, t.password, t.fullName, role: r)))
            .Bind(t => User.From(Guid.NewGuid(), t.email, t.fullName, t.password, new[] { t.role.RoleName }))
            .Bind(user => _usersRepository.RegisterUserAsync(user))
            .Map(_ => Unit.Value);
    }
}
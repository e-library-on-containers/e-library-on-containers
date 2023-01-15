﻿using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Repositories;
using eLibraryOnContainers.Identity.Application.Services;
using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;
using MediatR;

namespace eLibraryOnContainers.Identity.Application.Users;

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

    public async Task<Result<Unit, ApplicationError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await Email.From(request.Email)
                .Ensure(async email => (await _usersRepository.GetUserByEmailAsync(email)).IsFailure, new UserAlreadyExists())
            .Bind(email => HashedPassword.FromRawPassword(request.Password, HashedPassword.DefaultHasher)
                .Map(password => (email, password)))
            .Bind(t => FullName.From(request.FirstName, request.LastName)
                .Map(f => (t.email, t.password, fullName: f)))
            .Bind(t => _rolesService.GetDefaultRoleAsync()
                .Map(r => (t.email, t.password, t.fullName, role: r)))
            .Bind(t => User.From(Guid.NewGuid(), t.email, t.fullName, t.password, new[] { t.role.RoleName }))
            .Bind(user => _usersRepository.RegisterUserAsync(user))
            .Map(_ => Unit.Value);
    }
}
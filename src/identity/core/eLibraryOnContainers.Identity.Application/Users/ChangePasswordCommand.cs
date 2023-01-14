using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Application.Repositories;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;
using MediatR;

namespace eLibraryOnContainers.Identity.Application.Users;

public class ChangePasswordCommand : IRequest<Result<Unit, ApplicationError>>
{
    public string Email { get; set; }
    public string OldPassword { get; }
    public string NewPassword { get; }

    public ChangePasswordCommand(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<Unit, ApplicationError>>
{
    private readonly IUsersRepository _usersRepository;

    public ChangePasswordCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<Unit, ApplicationError>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return await Email.From(request.Email)
            .Bind(email => _usersRepository.GetUserByEmailAsync(email)
                .Bind(user => HashedPassword.FromRawPassword(request.OldPassword, HashedPassword.DefaultHasher)
                    .Map(challengePassword => (user, challengePassword))))
            .Bind(t => HashedPassword.FromRawPassword(request.NewPassword, HashedPassword.DefaultHasher)
                .Map(newPassword => (t.user, t.challengePassword, newPassword)))
            .Bind(t => t.user.WithNewPassword(t.challengePassword, t.newPassword))
            .Bind(user => _usersRepository.UpdatePasswordAsync(user))
            .Map(_ => Unit.Value);
    }
}
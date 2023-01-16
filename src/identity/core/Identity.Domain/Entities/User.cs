using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.Extensions;
using eLibraryOnContainers.Identity.Domain.ValueObjects;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Entities;

public class User : Entity<Guid>
{
    public Email Email { get; }
    public FullName FullName { get; }
    public HashedPassword Password { get; }
    public IReadOnlyCollection<RoleName> Roles { get; set; }

    private User(Guid id, Email email, FullName fullName, HashedPassword password, IReadOnlyCollection<RoleName> roles) : base(id)
    {
        Email = email;
        FullName = fullName;
        Password = password;
        Roles = roles;
    }

    public static Result<User, ApplicationError> From(Guid id, Email email, FullName fullName, HashedPassword password,
        IReadOnlyCollection<RoleName> roles) =>
        roles.AsApplicationResult()
            .Ensure(x => x != null && x.Count != 0, new NoUserRoles())
            .Map(x => new User(id, email, fullName, password, x));

    public Result<User, ApplicationError> WithNewPassword(HashedPassword oldPasswordCandidate,
        HashedPassword newPassword) =>
        oldPasswordCandidate.AsApplicationResult()
            .Ensure(x => x.Equals(Password), new PasswordsDoesNotMatch())
            .Map(_ => new User(Id, Email, FullName, newPassword, Roles));
}
using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.ValueObjects;

namespace eLibraryOnContainers.Identity.Tests;

public static class New
{
    public static HashedPassword HashedPassword(string rawPassword) =>
        Domain.ValueObjects.HashedPassword.FromRawPassword(rawPassword, Functions.HashPassword).Value;

    public static RoleName RoleName(string value) => Domain.ValueObjects.RoleName.From(value).Value;

    public static User User(Guid? id = null, string email = null, string password = null, string firstName = null,
        string lastName = null, string role = null)
    {
        return Domain.Entities.User.From(id ?? Guid.NewGuid(), Email.From(email ?? "mail@domain.com").Value,
            FullName.From(firstName ?? "first", lastName ?? "last").Value, HashedPassword(password ?? "Password1234"),
            new[] { RoleName(role ?? "User") }).Value;
    }
}
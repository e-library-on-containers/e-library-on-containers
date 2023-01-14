using eLibraryOnContainers.Identity.Domain.Entities;
using eLibraryOnContainers.Identity.Domain.ValueObjects;

namespace eLibraryOnContainers.Identity.Tests;

public static class New
{
    public static HashedPassword HashedPassword(string rawPassword) =>
        Domain.ValueObjects.HashedPassword.FromRawPassword(rawPassword, Functions.HashPassword).Value;

    public static RoleName RoleName(string value) => Domain.ValueObjects.RoleName.From(value).Value;
}
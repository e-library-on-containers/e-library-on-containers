using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.ValueObjects;

namespace eLibraryOnContainers.Identity.Domain.Entities;

public class Role : Entity<Guid>
{
    public RoleName RoleName { get; }

    public Role(Guid id, RoleName roleName) : base(id)
    {
        RoleName = roleName;
    }
}
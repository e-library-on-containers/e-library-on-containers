using CSharpFunctionalExtensions;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities;

public class Role : Entity<Guid>
{
    public RoleName RoleName { get; }

    public Role(Guid id, RoleName roleName) : base(id)
    {
        RoleName = roleName;
    }
}
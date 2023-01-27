namespace Identity.Infrastructure.Models;

public class ReadRole
{
    public Guid UserId { get; }
    public string RoleName { get; }

    public ReadRole(Guid userId, string roleName)
    {
        UserId = userId;
        RoleName = roleName;
    }
}
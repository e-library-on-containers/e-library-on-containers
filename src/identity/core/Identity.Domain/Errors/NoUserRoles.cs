using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class NoUserRoles : ValidationError
{
    public NoUserRoles() : base("Can not create user without any roles.")
    {
    }
}
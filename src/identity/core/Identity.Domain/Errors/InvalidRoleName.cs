using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class InvalidRoleName : ValidationError
{
    public InvalidRoleName(string roleName) : base($"Role name \"{roleName}\" is invalid.")
    {
    }
}
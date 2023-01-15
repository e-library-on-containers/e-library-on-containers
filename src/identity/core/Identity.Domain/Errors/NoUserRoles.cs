using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class NoUserRoles : ValidationError
{
    public NoUserRoles() : base("Can not create user without any roles.")
    {
    }
}
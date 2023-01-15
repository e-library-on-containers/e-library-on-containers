using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class InvalidPassword : ValidationError
{
    public InvalidPassword() : base("Password is invalid.")
    {
    }
}
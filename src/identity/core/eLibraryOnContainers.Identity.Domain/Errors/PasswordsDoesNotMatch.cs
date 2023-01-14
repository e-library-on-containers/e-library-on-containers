using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class PasswordsDoesNotMatch : ValidationError
{
    public PasswordsDoesNotMatch() : base("Password does not match.")
    {
    }
}
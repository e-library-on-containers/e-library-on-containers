using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class UnsafePassword : ValidationError
{
    public UnsafePassword() : base("Password is not safe enough.")
    {
    }
}
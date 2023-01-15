using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class InvalidEmail : ValidationError
{
    public InvalidEmail(string address) : base($"Email address \"{address}\" is invalid.")
    {
    }
}
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Errors;

public class InvalidFullName : ValidationError
{
    public InvalidFullName(string firstName, string lastName) : base($"Name \"{firstName} {lastName}\" is invalid.")
    {
    }
}
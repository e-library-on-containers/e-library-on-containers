using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class InvalidFullName : ValidationError
{
    public InvalidFullName(string firstName, string lastName) : base($"Name \"{firstName} {lastName}\" is invalid.")
    {
    }
}
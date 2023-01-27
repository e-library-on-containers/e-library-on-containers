using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class InvalidEmail : ValidationError
{
    public InvalidEmail(string address) : base($"Email address \"{address}\" is invalid.")
    {
    }
}
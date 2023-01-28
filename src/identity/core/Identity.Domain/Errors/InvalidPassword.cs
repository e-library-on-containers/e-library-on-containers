using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class InvalidPassword : ValidationError
{
    public InvalidPassword() : base("Password is invalid.")
    {
    }
}
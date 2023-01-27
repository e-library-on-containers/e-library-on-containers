using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class PasswordsDoesNotMatch : ValidationError
{
    public PasswordsDoesNotMatch() : base("Passwords does not match.")
    {
    }
}
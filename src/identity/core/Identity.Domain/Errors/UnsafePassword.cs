using FunctionalValidation.Errors;

namespace Identity.Domain.Errors;

public class UnsafePassword : ValidationError
{
    public UnsafePassword() : base("Password is not safe enough.")
    {
    }
}
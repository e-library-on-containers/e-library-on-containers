namespace FunctionalValidation.Errors
{
    public class RestrictedAccessError : ApplicationError
    {
        public RestrictedAccessError(string message) : base(ErrorType.RestrictedAccess, message) { }
    }
}
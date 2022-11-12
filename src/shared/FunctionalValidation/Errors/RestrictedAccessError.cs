namespace FunctionalValidation.Errors
{
    /// <summary>
    /// <see cref="ApplicationError"/> with <see cref="ApplicationError.Type"/> equal to <see cref="ErrorType.RestrictedAccess"/>.
    /// </summary>
    public class RestrictedAccessError : ApplicationError
    {
        public RestrictedAccessError(string message) : base(ErrorType.RestrictedAccess, message) { }
    }
}
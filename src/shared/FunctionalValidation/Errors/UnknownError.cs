namespace FunctionalValidation.Errors
{
    /// <summary>
    /// <see cref="ApplicationError"/> with <see cref="ApplicationError.Type"/> equal to <see cref="ErrorType.Unknown"/>.
    /// </summary>
    public class UnknownError : ApplicationError
    {
        public UnknownError(string message) : base(ErrorType.Unknown, message) { }
    }
}
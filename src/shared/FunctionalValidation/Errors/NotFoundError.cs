namespace FunctionalValidation.Errors
{
    /// <summary>
    /// <see cref="ApplicationError"/> with <see cref="ApplicationError.Type"/> equal to <see cref="ErrorType.NotFound"/>.
    /// </summary>
    public class NotFoundError : ApplicationError
    {
        public NotFoundError(string message) : base(ErrorType.NotFound, message) { }
    }
}
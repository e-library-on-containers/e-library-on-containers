namespace FunctionalValidation.Errors
{
    /// <summary>
    /// <see cref="ApplicationError"/> with <see cref="ApplicationError.Type"/> equal to <see cref="ErrorType.Validation"/>.
    /// </summary>
    public class ValidationError : ApplicationError
    {
        public ValidationError(string message) : base(ErrorType.Validation, message) { }
    }
}
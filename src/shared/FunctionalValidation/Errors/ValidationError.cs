namespace FunctionalValidation.Errors
{
    public class ValidationError : ApplicationError
    {
        public ValidationError(string message) : base(ErrorType.Validation, message) { }
    }
}
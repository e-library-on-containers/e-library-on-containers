namespace FunctionalValidation.Errors
{
    public class NotFoundError : ApplicationError
    {
        public NotFoundError(string message) : base(ErrorType.NotFound, message) { }
    }
}
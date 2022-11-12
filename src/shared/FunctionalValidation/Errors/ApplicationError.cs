using System;

namespace FunctionalValidation.Errors
{
    public abstract class ApplicationError : Exception
    {
        public ErrorType Type { get; }

        protected ApplicationError(ErrorType type, string message) : base(message)
        {
            Type = type;
        }
    }
}
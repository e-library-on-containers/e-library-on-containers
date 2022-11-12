using System;

namespace FunctionalValidation.Errors
{
    /// <summary>
    /// Abstract class that determines application error.
    /// </summary>
    public abstract class ApplicationError : Exception
    {
        /// <summary>
        /// The type of error.
        /// </summary>
        public ErrorType Type { get; }

        protected ApplicationError(ErrorType type, string message) : base(message)
        {
            Type = type;
        }
    }
}
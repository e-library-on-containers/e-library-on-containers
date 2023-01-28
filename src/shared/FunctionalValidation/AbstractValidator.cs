using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace FunctionalValidation
{
    /// <summary>
    /// Type that defines abstract method for validating value of <typeparamref name="TType"/>.
    /// </summary>
    /// <typeparam name="TType">The type that is validated by implementation of this abstract class.</typeparam>
    public abstract class AbstractValidator<TType>
    {
        /// <summary>
        /// Validates value of <typeparamref name="TType"/> stored in <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The result that contains value of <typeparamref name="TType"/> if success or <see cref="ValidationError"/> if failure.</param>
        /// <returns>The result that contains value of <typeparamref name="TType"/> if validation succeeded or <see cref="ValidationError"/> if validation failed.</returns>
        public abstract Result<TType, ApplicationError> Validate(Result<TType, ApplicationError> result);
        
        internal Result<TType, ApplicationError> Validate(TType value)
        {
            return Validate(Result.Success<TType, ApplicationError>(value));
        }
    }
}
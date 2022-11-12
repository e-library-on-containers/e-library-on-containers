using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace FunctionalValidation
{
    /// <summary>
    /// Interface that allows validation of all types having their implementation of <see cref="AbstractValidator{TType}"/>.
    /// </summary>
    public interface IFunctionalValidator
    {
        /// <summary>
        /// Validates <paramref name="value"/> and returns <see cref="Result{TType}"/> that contains <paramref name="value"/> if validation is successful or error message if validation fails.
        /// </summary>
        /// <param name="value">Value that is validated.</param>
        /// <typeparam name="TType">Type that is validated by implementation of this interface.</typeparam>
        /// <returns>Result of validation.</returns>
        Result<TType, ApplicationError> Validate<TType>(TType value);
    }
}
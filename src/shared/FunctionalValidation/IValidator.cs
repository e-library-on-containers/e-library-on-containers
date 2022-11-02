using CSharpFunctionalExtensions;

namespace FunctionalValidation
{
    /// <summary>
    /// Interface that defines validator for <typeparamref name="TType"/> type.
    /// </summary>
    /// <typeparam name="TType">Type that is validated by implementation of this interface.</typeparam>
    public interface IValidator<TType>
    {
        /// <summary>
        /// Validates <paramref name="value"/> and returns <see cref="Result{TType}"/> that contains <paramref name="value"/> if validation is successful or error message if validation fails.
        /// </summary>
        /// <param name="value">Value that is validated.</param>
        /// <returns>Result of validation.</returns>
        Result<TType> Validate(TType value);
    }
}
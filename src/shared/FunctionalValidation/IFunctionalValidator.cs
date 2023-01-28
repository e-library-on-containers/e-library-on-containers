using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace FunctionalValidation
{
    /// <summary>
    /// Type that exposes method for validation.
    /// </summary>
    public interface IFunctionalValidator
    {
        /// <summary>
        /// Validates <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value that is validated.</param>
        /// <typeparam name="TType">The type of validated <paramref name="value"/>.</typeparam>
        /// <returns>The result that contains <paramref name="value"/> if validation succeeded or <see cref="ApplicationError"/> if validation failed.</returns>
        /// <seealso href="https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/2a69d589793817793f87c470535368f414196e79/CSharpFunctionalExtensions/Result/ResultTE.cs">Result TType, TError</seealso>
        // TODO add < and > to Result<TType, TError> in <seealso/> after https://github.com/dotnet/docfx/issues/2723 is fixed
        Result<TType, ApplicationError> Validate<TType>(TType value);
    }
}
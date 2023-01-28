using System;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace FunctionalValidation.Extensions
{
    public static class ResultErrorExtensions
    {
        /// <summary>
        /// Returns a new failure result if the predicate is false. Otherwise returns the starting result.
        /// </summary>
        /// <typeparam name="TType">The type of validated value.</typeparam>
        /// <param name="result">The result that stores <typeparamref name="TType"/> if success or <see cref="ApplicationError"/> if failure.</param>
        /// <param name="predicate">The function that determines if value is valid.</param>
        /// <param name="message">The error message passed into <see cref="ValidationError"/> on failure.</param>
        /// <returns>The result that contains value of <typeparamref name="TType"/> if <paramref name="predicate"/> succeeded or <see cref="ValidationError"/> if <paramref name="predicate"/> failed.</returns>
        public static Result<TType, ApplicationError> Ensure<TType>(this Result<TType, ApplicationError> result,
            Func<TType, bool> predicate, string message) => result.Ensure(predicate, new ValidationError(message));
    }
}
using System;
using System.Net;
using FunctionalValidation.Errors;

namespace FunctionalValidation.Extensions
{
    public static class ApplicationErrorExtensions
    {
        /// <summary>
        /// Maps <paramref name="error"/> to <see href="https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netstandard2.1">HttpStatusCode</see> based on <see cref="ApplicationError.Type"/>.
        /// </summary>
        /// <param name="error">The error that will be mapped to appropriate <see href="https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netstandard2.1">HttpStatusCode</see>.</param>
        /// <returns>The <see href="https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netstandard2.1">HttpStatusCode</see> for <paramref name="error"/>.</returns>
        /// <exception cref="ArgumentException">Could not map <paramref name="error"/> to appropriate <see href="https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netstandard2.1">HttpStatusCode</see>.</exception>
        public static HttpStatusCode ToHttpStatusCode(this ApplicationError error)
        {
            return error.Type switch
            {
                ErrorType.NotFound => HttpStatusCode.NotFound,
                ErrorType.Validation => HttpStatusCode.BadRequest,
                ErrorType.RestrictedAccess => HttpStatusCode.Forbidden,
                _ => throw new ArgumentException($"Unknown error type: {error.Type}")
            };
        }
    }
}
using System;
using System.Net;
using FunctionalValidation.Errors;

namespace FunctionalValidation.Extensions
{
    public static class ApplicationErrorExtensions
    {
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
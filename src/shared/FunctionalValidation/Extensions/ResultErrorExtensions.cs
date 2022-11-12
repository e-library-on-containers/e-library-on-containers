using System;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace FunctionalValidation.Extensions
{
    public static class ResultErrorExtensions
    {
        public static Result<TType, ApplicationError> Ensure<TType>(this Result<TType, ApplicationError> result,
            Func<TType, bool> predicate, string message) => result.Ensure(predicate, new ValidationError(message));
    }
}
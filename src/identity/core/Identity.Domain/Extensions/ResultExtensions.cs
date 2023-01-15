using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.Extensions;

public static class ResultExtensions
{
    public static Result<TType, ApplicationError> AsApplicationResult<TType>(this TType value) =>
        Result.Success<TType, ApplicationError>(value);
}
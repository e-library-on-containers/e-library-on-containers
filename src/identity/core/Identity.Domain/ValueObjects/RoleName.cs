using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Domain.Errors;
using eLibraryOnContainers.Identity.Domain.Extensions;
using FunctionalValidation.Errors;

namespace eLibraryOnContainers.Identity.Domain.ValueObjects;

public class RoleName : ValueObject
{
    public string Value { get; }

    private RoleName(string value)
    {
        Value = value;
    }

    public static Result<RoleName, ApplicationError> From(string value) =>
        value.AsApplicationResult()
            .Ensure(x => !string.IsNullOrWhiteSpace(x), x => new InvalidRoleName(x))
            .Map(x => new RoleName(x));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Errors;
using Identity.Domain.Extensions;

namespace Identity.Domain.ValueObjects;

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
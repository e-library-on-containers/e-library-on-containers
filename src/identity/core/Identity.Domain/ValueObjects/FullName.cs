using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Errors;
using Identity.Domain.Extensions;

namespace Identity.Domain.ValueObjects;

public class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName, ApplicationError> From(string firstName, string lastName) =>
        (firstName, lastName).AsApplicationResult()
            .Ensure(t => !string.IsNullOrWhiteSpace(t.firstName) && !string.IsNullOrWhiteSpace(t.lastName),
                t => new InvalidFullName(t.firstName, t.lastName))
            .Map(t => new FullName(t.firstName, t.lastName));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}
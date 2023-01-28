using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Errors;
using Identity.Domain.Extensions;

namespace Identity.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    public static Result<Email, ApplicationError> From(string address) =>
        address.AsApplicationResult()
            .Ensure(x => !string.IsNullOrWhiteSpace(x), x => new InvalidEmail(x))
            .Ensure(x => Regex.IsMatch(x, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase),
                x => new InvalidEmail(x))
            .Map(s => new Email(s));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}
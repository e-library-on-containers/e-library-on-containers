using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Identity.Domain.Errors;
using Identity.Domain.Extensions;

namespace Identity.Domain.ValueObjects;

public class HashedPassword : ValueObject
{
    public const int MIN_LENGTH = 8;
    public const int MAX_LENGTH = 32;

    public static readonly PasswordHasher DefaultHasher = value => Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));

    public delegate string PasswordHasher(string rawValue);

    public string Value { get; }

    private HashedPassword(string value)
    {
        Value = value;
    }

    public Result<bool, ApplicationError> Challenge(string passwordCandidate, PasswordHasher hasher)
    {
        return new HashedPassword(hasher(passwordCandidate))
            .AsApplicationResult()
            .Ensure(c => Equals(c), new PasswordsDoesNotMatch())
            .Map(_ => true);
    }

    public static Result<HashedPassword, ApplicationError> FromHashedPassword(string hashedPassword) =>
        hashedPassword.AsApplicationResult()
            .Ensure(x => !string.IsNullOrWhiteSpace(x), new InvalidPassword())
            .Map(x => new HashedPassword(x));

    public static Result<HashedPassword, ApplicationError> FromRawPassword(string rawPassword, PasswordHasher hasher) =>
        rawPassword.AsApplicationResult()
            .Ensure(x => !string.IsNullOrWhiteSpace(x) && !Regex.IsMatch(x, @$"^.{{{MAX_LENGTH + 1},}}$"), new InvalidPassword())
            .Ensure(x => Regex.IsMatch(x, @$"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{{{MIN_LENGTH},}}$"), new UnsafePassword())
            .Map(x => new HashedPassword(hasher(x)));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
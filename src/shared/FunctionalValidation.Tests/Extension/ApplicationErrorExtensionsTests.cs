using System.Net;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace FunctionalValidation.Tests.Extension;

[Subject(typeof(ApplicationErrorExtensions))]
internal class ApplicationErrorExtensionsTests
{
    static ApplicationError error;
    static HttpStatusCode result;

    Because of = () => result = error.ToHttpStatusCode();

    class When_NotFoundError
    {
        Establish ctx = () => error = new NotFoundError(string.Empty);

        It should_return_NotFound_status_code = () => result.ShouldEqual(HttpStatusCode.NotFound);
    }

    class When_ValidationError
    {
        Establish ctx = () => error = new ValidationError(string.Empty);

        It should_return_BadRequest_status_code = () => result.ShouldEqual(HttpStatusCode.BadRequest);
    }

    class When_RestrictedAccessError
    {
        Establish ctx = () => error = new RestrictedAccessError(string.Empty);

        It should_return_Forbidden_status_code = () => result.ShouldEqual(HttpStatusCode.Forbidden);
    }
}
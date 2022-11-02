using CSharpFunctionalExtensions;
using Machine.Fakes;
using Machine.Specifications;
using Result = CSharpFunctionalExtensions.Result;

namespace FunctionalValidation.Tests;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers
public class FakeTests : WithFakes
{
    static IValidator<string> validator;
    static Result<string> result;

    Establish ctx = () =>
    {
        validator = An<IValidator<string>>();
        validator.WhenToldTo(x => x.Validate(Param<string>.IsAnything))
            .Return(Result.Success(""));
    };

    Because of = () => result = validator.Validate("");

    It should_be_able_to_mock_validator = () => result.IsSuccess.ShouldBeTrue();
}
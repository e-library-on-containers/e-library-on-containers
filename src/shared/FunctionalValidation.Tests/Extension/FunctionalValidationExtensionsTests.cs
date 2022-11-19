using FunctionalValidation.Extensions;
using FunctionalValidation.Tests.ImplementedValidators;
using Machine.Specifications;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers

namespace FunctionalValidation.Tests.Extension;

[Subject(typeof(FunctionalValidationExtensions))]
public class FunctionalValidationExtensionsTests
{
    static ServiceProvider provider;

    Establish ctx = () =>
    {
        var services = new ServiceCollection();

        services.AddFunctionalValidation<FunctionalValidationExtensionsTests>();

        provider = services.BuildServiceProvider();
    };

    It should_resolve_all_implemented_validators = () => provider
        .GetServices<AbstractValidator<string>>().Select(x => x.GetType()).ShouldEqual(new[]
        {
            typeof(LongerThan5CharactersValidator),
            typeof(NotNullOrWhitespaceValidator)
        });

    It should_resolve_functional_validator = () =>
        provider.GetService<IFunctionalValidator>().ShouldNotBeNull();

}
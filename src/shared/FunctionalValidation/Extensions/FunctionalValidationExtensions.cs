using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalValidation.Extensions
{
    public static class FunctionalValidationExtensions
    {
        public static IServiceCollection AddFunctionalValidation<TType>(this IServiceCollection services) =>
            services.AddFunctionalValidation(typeof(TType).Assembly);

        public static IServiceCollection AddFunctionalValidation(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var validatorTypes = types
                .Where(t => t.BaseType is { IsGenericType: true } && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));

            foreach (var validatorType in validatorTypes)
            {
                services.AddSingleton(validatorType.BaseType!, validatorType);
            }

            services.AddSingleton<IFunctionalValidator, FunctionalValidator>();
            return services;
        }
    }
}
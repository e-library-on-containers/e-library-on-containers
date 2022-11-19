using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalValidation.Extensions
{
    public static class FunctionalValidationExtensions
    {
        /// <summary>
        /// Register <see cref="IFunctionalValidator"/> and all implementations of <see cref="AbstractValidator{TType}"/> found in assembly that contains <typeparamref name="TType"/>.
        /// </summary>
        /// <typeparam name="TType">The type existing in assembly that will be scanned for implementation of <see cref="AbstractValidator{TType}"/>.</typeparam>
        /// <param name="services">The service collection to which <see cref="IFunctionalValidator"/> and found implementations of <see cref="AbstractValidator{TType}"/> will be added.</param>
        /// <returns>The service collection passed as <paramref name="services"/>.</returns>
        public static IServiceCollection AddFunctionalValidation<TType>(this IServiceCollection services) =>
            services.AddFunctionalValidation(typeof(TType).Assembly);

        /// <summary>
        /// Register <see cref="IFunctionalValidator"/> and all implementations of <see cref="AbstractValidator{TType}"/> found in <paramref name="assembly"/>.
        /// </summary>
        /// <param name="services">The service collection to which <see cref="IFunctionalValidator"/> and found implementations of <see cref="AbstractValidator{TType}"/> will be added.</param>
        /// <param name="assembly">The assembly to be scanned for implementation of <see cref="AbstractValidator{TType}"/>.</param>
        /// <returns>The service collection passed as <paramref name="services"/>.</returns>
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
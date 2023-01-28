using System;
using System.Linq;
using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalValidation
{
    internal class FunctionalValidator : IFunctionalValidator
    {
        private readonly IServiceProvider _provider;

        public FunctionalValidator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Result<TType, ApplicationError> Validate<TType>(TType value)
        {
            var validators = _provider.GetServices<AbstractValidator<TType>>();

            return validators.Aggregate(Result.Success<TType, ApplicationError>(value),
                (result, validator) =>
                    result.Match(
                        validator.Validate,
                        _ => result)
            );
        }
    }
}
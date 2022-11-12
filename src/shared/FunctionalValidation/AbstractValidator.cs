using CSharpFunctionalExtensions;
using FunctionalValidation.Errors;

namespace FunctionalValidation
{
    public abstract class AbstractValidator<TType>
    {
        public abstract Result<TType, ApplicationError> Validate(Result<TType, ApplicationError> result);
        
        public Result<TType, ApplicationError> Validate(TType value)
        {
            return Validate(Result.Success<TType, ApplicationError>(value));
        }
    }
}
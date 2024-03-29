﻿using CSharpFunctionalExtensions;
using FunctionalValidation;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;
using Identity.Application.Authentication;

namespace Identity.Api.Validators
{
    public class SignInQueryValidator : AbstractValidator<SignInQuery>
    {
        public override Result<SignInQuery, ApplicationError> Validate(Result<SignInQuery, ApplicationError> result)
        {
            return result
                .Ensure(q => !string.IsNullOrWhiteSpace(q.Email), "E-mail address must not be empty.")
                .Ensure(q => !string.IsNullOrWhiteSpace(q.Password), "Password must not be empty.");
        }
    }
}

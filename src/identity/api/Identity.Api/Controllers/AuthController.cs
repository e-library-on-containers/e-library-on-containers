using FunctionalValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using Identity.Api.Common;
using Identity.Application.Authentication;

namespace Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ApplicationController
{
    private readonly IFunctionalValidator _functionalValidator;
    private readonly IMediator _mediator;

    public AuthController(IFunctionalValidator functionalValidator, IMediator mediator)
    {
        _functionalValidator = functionalValidator;
        _mediator = mediator;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(SignInQuery query) =>
        await MatchWithDefaultErrorHandler(
            _functionalValidator.Validate(query)
                .Bind(c => _mediator.Send(c)),
            dto => Ok(dto)
        );
}
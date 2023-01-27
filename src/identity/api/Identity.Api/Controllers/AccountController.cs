using System.Net;
using CSharpFunctionalExtensions;
using FunctionalValidation;
using Identity.Api.Common;
using Identity.Api.Extensions;
using Identity.Api.Request;
using Identity.Application.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ApplicationController
{
    private readonly IFunctionalValidator _functionalValidator;
    private readonly IMediator _mediator;

    public AccountController(IFunctionalValidator functionalValidator, IMediator mediator)
    {
        _functionalValidator = functionalValidator;
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserCommand command) =>
        await MatchWithDefaultErrorHandler(
            _functionalValidator.Validate(command)
                .Bind(c => _mediator.Send(c)),
            _ => StatusCode((int)HttpStatusCode.Created)
        );

    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest request) =>
        await MatchWithDefaultErrorHandler(
            _functionalValidator.Validate(request)
                .Bind(r => _mediator.Send(
                    new ChangePasswordCommand(HttpContext.GetUserEmail(), r.OldPassword, r.NewPassword))),
            _ => StatusCode((int)HttpStatusCode.Accepted)
        );
}
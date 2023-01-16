using System.Net;
using CSharpFunctionalExtensions;
using eLibraryOnContainers.Identity.Api.Responses;
using FunctionalValidation.Errors;
using FunctionalValidation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eLibraryOnContainers.Identity.Api.Common
{
    public class ApplicationController : ControllerBase
    {
        public IActionResult MatchWithDefaultErrorHandler<TIn>(Result<TIn, ApplicationError> result,
            Func<TIn, IActionResult> onSuccess)
        {
            return result.Match(onSuccess,
                error => error.ToHttpStatusCode() switch
                {
                    HttpStatusCode.InternalServerError => StatusCode(500),
                    var x => StatusCode((int)x, error.Message)
                });
        }

        public Task<IActionResult> MatchWithDefaultErrorHandler<TIn>(Task<Result<TIn, ApplicationError>> result,
            Func<TIn, IActionResult> onSuccess)
        {
            return result.Match(onSuccess,
                error => error.ToHttpStatusCode() switch
                {
                    HttpStatusCode.InternalServerError => StatusCode(500, new ErrorMessage("Something went wrong.")),
                    var x => StatusCode((int)x, new ErrorMessage(error.Message))
                });
        }
    }
}

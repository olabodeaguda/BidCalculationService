using BidCalculationService.Domain.Exceptions;
using BidCalculationService.Domain.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculationService.Api.Helpers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (exception == null) throw new ArgumentNullException(nameof(exception));

            _logger.LogError(exception, $"Exception occurred: {exception.Message}", new[] { exception.Source, exception.Message });

            ProblemDetails problemDetails = exception switch
            {
                ValidationException ex => new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = ex.Message,
                    Extensions = { ["errors"] = ex.Errors.Select(_ => new Error(_.ErrorCode ?? _.PropertyName, _.ErrorMessage)).ToArray() }
                },
                _ => new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = exception.Message,
                    Type = exception.GetType().Name
                }
            };

            Result result = Result.Failure(new Error(problemDetails.Title ?? "Error occured", problemDetails.Detail ?? "An error occured while processing the request"));
            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}

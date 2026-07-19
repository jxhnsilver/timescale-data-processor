using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimescaleDataProcessor.Api.Exceptions;

namespace TimescaleDataProcessor.Api.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, title, detail) = MapException(exception);

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(
                    exception, 
                    "Unhandled exception: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static (int StatusCode, string Title, string Detail) MapException(Exception exception) => exception switch
        {
            FileFormatException ex => (StatusCodes.Status400BadRequest, ex.ErrorCode, ex.Message),
            BusinessRuleViolationException ex => (StatusCodes.Status422UnprocessableEntity, ex.ErrorCode, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error", "An unexpected error occurred")
        };
    }
}

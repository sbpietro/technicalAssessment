using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using TopUp.Application;

namespace TopUp.Api
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var httpContext = context.HttpContext;

            _logger.LogError(exception, "Exception: {exceptionMessage}", exception.Message);

            ProblemDetails problemDetails;
            switch (exception)
            {
                case ApiApplicationException _:
                    problemDetails = new ProblemDetails()
                    {
                        Status = 400,
                        Detail = exception.Message,
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                        Instance = httpContext.Request.Path,
                        Title = "Application error",

                    };
                    break;

                default:
                    problemDetails = new ProblemDetails
                    {
                        Status = 500,
                        Title = "API Error",
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                        Detail = exception.Message,
                    };
                    break;
            }

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };

            context.ExceptionHandled = true;
        }
    }
}

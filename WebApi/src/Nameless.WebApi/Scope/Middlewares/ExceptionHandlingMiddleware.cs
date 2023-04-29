using Nameless.Core.Domain.Exceptions;
using System.Diagnostics;
using System.Text.Json;

namespace Nameless.WebApi.Scope.Middlewares
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            if (exception is DetailedException categorizedException)
            {
                await TreatCategorizedExceptionAsync(httpContext, categorizedException);
            }
            else
            {
                await TreatExceptionAsync(httpContext, exception);
            }
        }

        private static async Task TreatCategorizedExceptionAsync(HttpContext httpContext, DetailedException categorizedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                type = categorizedException.Type,
                error = categorizedException.Error,
                detail = categorizedException.Detail,
                instance = httpContext.Request.Path.Value,
                traceId = Activity.Current?.TraceId.ToString()
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task TreatExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                type = "InternalServerError",
                error = "InternalServerError",
                detail = exception.Message,
                instance = httpContext.Request.Path.Value,
                traceId = Activity.Current?.TraceId.ToString()
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

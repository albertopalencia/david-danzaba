using Azure.Core;
using RealState.Domain.Exceptions;
using System.Net;

namespace RealState.Api.Middleware;

public class AppExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppExceptionHandlerMiddleware> _logger;

    public AppExceptionHandlerMiddleware(RequestDelegate next, ILogger<AppExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error:",ex.Message);

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                ErrorMessage = ex.Message
            });

            context.Response.ContentType = ContentType.ApplicationJson.ToString();

            context.Response.StatusCode = ex switch
            {
                CoreBusinessException => (int)HttpStatusCode.BadRequest, 
                ArgumentException => (int)HttpStatusCode.NoContent,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await context.Response.WriteAsync(result);
        }
    }
}

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Yenilen.API.Shared;
using ValidationException = FluentValidation.ValidationException;

namespace Yenilen.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception caught by middleware");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        //default error code.
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var errors = new List<string> { exception.Message };
        
        //fluent errors.
        if (exception is ValidationException validationException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
        }
        
        //domain or application layer come error.
        else if (exception is KeyNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
        }

        var result = Result<string>.Failure(statusCode, errors);
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(result);
        await httpContext.Response.WriteAsync(json);
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger; // Logging service

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "Internal Server Error from the custom middleware.";
        var exceptionType = exception.GetType();
        if (exceptionType == typeof(DbUpdateException))
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = exception.Message;
        }
        else if (exceptionType == typeof(InvalidOperationException))
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = exception.Message;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            statusCode = StatusCodes.Status404NotFound;
            message = exception.Message;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            statusCode = StatusCodes.Status401Unauthorized;
            message = "Unauthorized Access";
        }
        else if (exceptionType == typeof(ValidationException))
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = exception.Message;
        }

        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(new
        {
            StatusCode = statusCode,
            Message = message
        });
    }
}
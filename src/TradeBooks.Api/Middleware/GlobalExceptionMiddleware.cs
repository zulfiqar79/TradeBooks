using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using TradeBooks.Application.Interfaces.Logging;

namespace TradeBooks.Api.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, IExceptionLogStore exceptionLogStore)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var severity = IsCriticalException(exception) ? LogLevel.Critical : LogLevel.Error;
            var safePath = SanitizeForLog(context.Request.Path);
            var safeMessage = RedactSensitiveData(exception.Message);
            var safeDetails = RedactSensitiveData(exception.ToString());
            logger.Log(severity, exception, "Unhandled exception on path {Path}. Message: {Message}", safePath, safeMessage);

            var entry = new ExceptionLogEntry(
                DateTime.UtcNow,
                severity.ToString(),
                safeMessage,
                safeDetails,
                safePath,
                context.User.Identity?.Name);

            await exceptionLogStore.StoreAsync(entry, context.RequestAborted);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected error",
                Detail = "An unexpected error occurred. Please try again later.",
                Type = "https://httpstatuses.com/500"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }

    private static bool IsCriticalException(Exception exception) =>
        exception is OutOfMemoryException or StackOverflowException or AccessViolationException or AppDomainUnloadedException;

    private static string SanitizeForLog(string? value) =>
        (value ?? string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);

    private static string RedactSensitiveData(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        var sanitized = Regex.Replace(value, "(?i)(authorization\\s*[:=]\\s*bearer\\s+)([^\\s;,\"]+)", "$1[REDACTED]");
        sanitized = Regex.Replace(sanitized, "(?i)(bearer\\s+)([^\\s;,\"]+)", "$1[REDACTED]");
        sanitized = Regex.Replace(sanitized, "(?i)(password\\s*[:=]\\s*)([^\\s;,\"&]+)", "$1[REDACTED]");
        sanitized = Regex.Replace(sanitized, "(?i)(token\\s*[:=]\\s*)([^\\s;,\"&]+)", "$1[REDACTED]");
        return sanitized;
    }
}

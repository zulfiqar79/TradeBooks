using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TradeBooks.Application.Interfaces.Logging;

namespace TradeBooks.Api.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IConfiguration configuration)
{
    private readonly LogLevel _configuredLogLevel = ParseLogLevel(configuration["ExceptionHandling:LogLevel"]);

    public async Task InvokeAsync(HttpContext context, IExceptionLogStore exceptionLogStore)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var safePath = SanitizeForLog(context.Request.Path);
            logger.Log(_configuredLogLevel, exception, "Unhandled exception on path {Path}", safePath);

            var entry = new ExceptionLogEntry(
                DateTime.UtcNow,
                _configuredLogLevel.ToString(),
                exception.Message,
                exception.ToString(),
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

    private static LogLevel ParseLogLevel(string? value) =>
        Enum.TryParse<LogLevel>(value, true, out var result) ? result : LogLevel.Error;

    private static string SanitizeForLog(string? value) =>
        (value ?? string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
}

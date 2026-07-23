using System.Security.Cryptography;
using System.Text;
using TradeBooks.AuditService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AuditStorage>();

var app = builder.Build();

var internalApiKeyEnvVar = builder.Configuration["Security:InternalApiKeyEnvVar"] ?? "TRADEBOOKS_AUDIT_API_KEY";
var internalApiKeyHeader = builder.Configuration["Security:InternalApiKeyHeader"] ?? "X-Internal-Api-Key";
var internalApiKey = Environment.GetEnvironmentVariable(internalApiKeyEnvVar);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/api/audit-records", StringComparison.OrdinalIgnoreCase))
    {
        await next();
        return;
    }

    if (string.IsNullOrWhiteSpace(internalApiKey))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
        return;
    }

    if (!context.Request.Headers.TryGetValue(internalApiKeyHeader, out var providedApiKey))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
        return;
    }

    if (!SecureEquals(internalApiKey, providedApiKey.ToString()))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
        return;
    }

    await next();
});
app.MapControllers();

app.Run();

static bool SecureEquals(string expected, string provided)
{
    var expectedBytes = Encoding.UTF8.GetBytes(expected);
    var providedBytes = Encoding.UTF8.GetBytes(provided);
    return CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes);
}

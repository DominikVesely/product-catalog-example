using Microsoft.AspNetCore.Mvc;

namespace Api.Classes;

/// <summary>
/// Default ProblemDetails response including common tracing properties.
/// </summary>
public class DefaultProblemDetails : ProblemDetails
{
    // The base ProblemDetails uses a property bag for extensions, 
    // but we define them as static properties here for Swagger to see them.

    /// <summary>
    /// A unique identifier for this specific request instance (HttpContext.TraceIdentifier).
    /// </summary>
    /// <example>0HMQ12345ABC</example>
    public string? RequestId { get; set; }

    /// <summary>
    /// The unique trace ID from the Activity/Diagnostic Source (the span ID for distributed tracing).
    /// </summary>
    /// <example>00-8fe6447814b7e24b4c10c1f544521473-b31a3962b0e6c543-00</example>
    public string? TraceId { get; set; }
}
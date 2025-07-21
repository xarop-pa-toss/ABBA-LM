using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LMWebAPI.Resources.Errors;

[Serializable]
public class ProblemException(int statusCode, string error, string message, object? context = null) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
    public string DefaultError { get; } = "Unspecified error";
    public string Error { get; } = error;
    public override string Message { get; } = message;
    public object? Context { get; } = context;
    public string? TraceId { get; set; }
    public Dictionary<string, object> Extensions { get; } = new Dictionary<string, object>();

}

public class ProblemExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
    

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {
        if (ex is not ProblemException)
        {
            // We don't want to catch other types of Exceptions
            return true;
        }

        var problemEx = (ProblemException)ex;
        var problemDetails = CreateProblemDetails(problemEx, httpContext);

        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
    }

    private ProblemDetails CreateProblemDetails(ProblemException problemEx, HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Status = problemEx.StatusCode,
            Title = problemEx.Error,
            Detail = problemEx.Message,
            Type = GetProblemTypeUri(problemEx),
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions.Add("traceId", problemEx.TraceId);
        problemDetails.Extensions.Add("timestamp", DateTimeOffset.UtcNow);
        if (problemEx.Context != null)
        {
            problemDetails.Extensions.Add("context", problemEx.Context);
        }

        foreach (var (key, value) in problemEx.Extensions)
        {
            problemDetails.Extensions.Add(key, value);
        }

        return problemDetails;
    }
    
    private static string GetProblemTypeUri(ProblemException ex)
    {
        // You can create a mapping or use a base URI
        return ex.StatusCode switch
        {
            400 => "https://httpstatuses.com/400",
            404 => "https://httpstatuses.com/404",
            409 => "https://httpstatuses.com/409",
            500 => "https://httpstatuses.com/500",
            _ => "https://httpstatuses.com/" + ex.StatusCode
        };
    }
}
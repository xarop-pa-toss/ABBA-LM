using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LMWebAPI.Resources.Errors;

[Serializable]
public class ProblemException : Exception
{
    public int StatusCode { get; }
    public string DefaultError { get; }
    public string Error { get; }
    public string Message { get; }

    public ProblemException(int statusCode, string error, string message) : base(message)
    {
        StatusCode = statusCode;
        DefaultError = "Unspecified error";
        Error = error;
        Message = message;
    }
}

public class ProblemNotFoundException : ProblemException
{
    private const string DefaultError = "Not Found";
    public ProblemNotFoundException(string message)
        : base(StatusCodes.Status404NotFound, DefaultError, message)
    {
    }

    public ProblemNotFoundException(string error, string message)
        : base(StatusCodes.Status404NotFound, error, message)
    {
    }
}

public class ProblemConflictException : ProblemException
{
    private const string DefaultError = "Conflict";

    public ProblemConflictException(string message)
        : base(StatusCodes.Status409Conflict, DefaultError, message)
    {
    }

    public ProblemConflictException(string error, string message)
        : base(StatusCodes.Status409Conflict, error, message)
    {
    }
}

public class ProblemBadRequestException : ProblemException
{
    private const string DefaultError = "Bad Request";

    public ProblemBadRequestException(string message)
        : base(StatusCodes.Status400BadRequest, DefaultError, message)
    {
    }

    public ProblemBadRequestException(string error, string message)
        : base(StatusCodes.Status400BadRequest, error, message)
    {
    }
}

public class ProblemNoChangeException : ProblemException
{
    private const string DefaultError = "Not Modified";

    public ProblemNoChangeException(string message)
        : base(StatusCodes.Status304NotModified, DefaultError, message)
    {
    }

    public ProblemNoChangeException(string error, string message)
        : base(StatusCodes.Status304NotModified, error, message)
    {
    }
}

public class ProblemDatabaseException : ProblemException
{
    private const string DefaultError = "Internal Server Error";

    public ProblemDatabaseException(string message)
        : base(StatusCodes.Status500InternalServerError, DefaultError, message)
    {
    }

    public ProblemDatabaseException(string error, string message)
        : base(StatusCodes.Status500InternalServerError, error, message)
    {
    }
}


public class ProblemExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ProblemExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ProblemException problemException)
        {
            return true;
        }

        var problemDetails = new ProblemDetails
        {
            Status = problemException.StatusCode,
            Title = problemException.Error,
            Detail = problemException.Message,
            Type = problemException.DefaultError
        };

        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            });
    }
    

}
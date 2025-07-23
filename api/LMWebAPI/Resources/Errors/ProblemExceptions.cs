namespace LMWebAPI.Resources.Errors;

/// <summary>
/// HTTP 400 Bad Request (client error such as bad syntax, invalid characters, exceeding file size limits, etc.).
/// </summary>
public class Problem400BadRequestException : ProblemException
{
    private new const string DefaultError = "Bad Request";

    public Problem400BadRequestException(string message, object? context = null)
        : base(StatusCodes.Status400BadRequest, DefaultError, message, context)
    {
    }

    public Problem400BadRequestException(string error, string message, object? context = null)
        : base(StatusCodes.Status400BadRequest, error, message, context)
    {
    }
}

/// <summary>
/// For HTTP 404 Not Found (resource not found on server)
/// </summary>
public class Problem404NotFoundException : ProblemException
{
    private new const string DefaultError = "Not Found";
    public Problem404NotFoundException(string message, object? context = null)
        : base(StatusCodes.Status404NotFound, DefaultError, message, context)
    {
    }

    public Problem404NotFoundException(string error, string message, object? context = null)
        : base(StatusCodes.Status404NotFound, error, message, context)
    {
    }
}

/// <summary>
/// For HTTP 409 Conflict (conflict with resources current state on server)
/// </summary>

public class Problem409ConflictException : ProblemException
{
    private new const string DefaultError = "Conflict";

    public Problem409ConflictException(string message, object? context = null)
        : base(StatusCodes.Status409Conflict, DefaultError, message, context)
    {
    }

    public Problem409ConflictException(string error, string message, object? context = null)
        : base(StatusCodes.Status409Conflict, error, message, context)
    {
    }
}

/// <summary>
/// For HTTP 500 Internal Server Error (generic server error)
/// </summary>
public class Problem500InternalServerErrorException : ProblemException
{
    private new const string DefaultError = "Internal Server Error";

    public Problem500InternalServerErrorException(string message, object? context = null)
        : base(StatusCodes.Status500InternalServerError, DefaultError, message, context)
    {
    }

    public Problem500InternalServerErrorException(string error, string message, object? context = null)
        : base(StatusCodes.Status500InternalServerError, error, message, context)
    {
    }
}

/// <summary>
/// For HTTP 503 Unavailable (when a server service is unreachable or offline)
/// </summary>
public class Problem503ServiceUnavailableException : ProblemException
{
    private new const string DefaultError = "Service Unavailable";

    public Problem503ServiceUnavailableException(string message, object? context = null)
        : base(StatusCodes.Status500InternalServerError, DefaultError, message, context)
    {
    }

    public Problem503ServiceUnavailableException(string error, string message, object? context = null)
        : base(StatusCodes.Status500InternalServerError, error, message, context)
    {
    }
}
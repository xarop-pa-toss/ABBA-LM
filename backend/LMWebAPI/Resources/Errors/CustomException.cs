namespace LMWebAPI.Resources.Errors;

public class CustomException(int statusCode, string message, Exception innerException = null) : Exception
{
    public int StatusCode { get; } = statusCode;
    public string Message { get; } = message;
    public Exception InnerException { get; } = innerException;
}
public class NotFoundException(string message) 
    : CustomException(StatusCodes.Status404NotFound, message)
{
}

public class ConflictException(string message) 
    : CustomException(StatusCodes.Status409Conflict, message)
{
}

public class BadRequestException(string message) 
    : CustomException(StatusCodes.Status400BadRequest, message)
{
}

public class RepositoryError(string message)
    : CustomException(StatusCodes.Status500InternalServerError, message)
{
}
namespace LMWebAPI.Resources;

public class DocumentNotFoundException : Exception
{
    public DocumentNotFoundException(string message) : base(message) { }
}

public class NoChangeException : Exception
{
    public NoChangeException(string message) : base(message) { }
}

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message) { }
}
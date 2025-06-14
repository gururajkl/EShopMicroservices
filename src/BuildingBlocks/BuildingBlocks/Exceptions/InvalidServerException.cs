namespace BuildingBlocks.Exceptions;

public class InvalidServerException : Exception
{
    public InvalidServerException(string message) : base(message) { }

    public InvalidServerException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}

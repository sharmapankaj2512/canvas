namespace Canvas;

public record Error
{
    public string Message { get; }

    private Error(string message)
    {
        Message = message;
    }

    public static Error New(string message)
    {
        return new Error(message);
    }
}
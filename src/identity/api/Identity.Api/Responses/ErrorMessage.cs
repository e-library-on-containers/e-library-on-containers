namespace eLibraryOnContainers.Identity.Api.Responses;

public class ErrorMessage
{
    public string Message { get; }

    public ErrorMessage(string message)
    {
        Message = message;
    }
}
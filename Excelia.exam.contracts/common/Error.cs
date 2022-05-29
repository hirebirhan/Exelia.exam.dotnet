namespace Excelia.exam.contracts.common;

public class Error
{
    public Error(string message, string description)
    {
        Message = message;
        Description = description;
    }

    public string Message { get; set; }
    public string Description { get; set; }
}
namespace MeChat.Common.Shared.Commons;
public class ValidationErorr
{
    public string Property { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public ValidationErorr(string property, string message)
    {
        Property = property;
        Message = message;
    }
}

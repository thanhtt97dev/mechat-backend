namespace MeChat.Common.UseCases.V1.Notification;
public class Response
{
    public record Notification(Guid Id, DateTime CreatedDate, string? Content, string? Image, string? Link, bool IsReaded);
}

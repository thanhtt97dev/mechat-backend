namespace MeChat.Common.UseCases.V1.Notification;
public class Response
{
    #region Notification
    public record Notification
    {
        public Guid Id { get; init; }
        public DateTime CreatedDate { get; init; }
        public int Type { get; init; }
        public Guid? RequesterId { get; init; }
        public string? RequesterName { get; init; }
        public string? Image { get; init; }
        public string? Link { get; init; }
        public bool IsReaded { get; init; } = false;  // Default value

        // Parameterless constructor
        public Notification() { }
    }
    #endregion


}

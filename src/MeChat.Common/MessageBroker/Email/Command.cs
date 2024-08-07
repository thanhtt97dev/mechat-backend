using MeChat.Common.Abstractions.Messages.InterationEvents;

namespace MeChat.Common.MessageBroker.Email;
public static class Command
{
    public record SendEmail() : ICommandMessage
    {
        public Guid Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}

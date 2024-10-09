using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class Message : EntityBase<Guid>, IDateTracking
{
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Type { get; set; }
    public bool IsDelete { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }

    public virtual User? User { get; set;}
    public virtual Conversation? Conversation { get; set; }
}

using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class UserConversation: Entity
{
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }
    public string NickName { get; set; } = string.Empty;
    public Guid? AdderId { get; set; }
    public int Status { get; set; }
    public bool IsRead { get; set; }
    public DateTime JoinedDate { get; set; }
    public DateTime LeaveDate { get; set; }


    public virtual User? User { get; set; }
    public virtual Conversation? Conversation { get; set; }
    public virtual User? Adder { get; set; }
}

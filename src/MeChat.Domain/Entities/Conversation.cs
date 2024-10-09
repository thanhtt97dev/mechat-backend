using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class Conversation : EntityBase<Guid>, IDateTracking, IUserTracking
{
    public string Name { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public int Type { get;set; }
    public int Accessibility { get;set; }
    public Guid? AdministratorId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }

    public virtual User? Administrator { get; set; }
    public virtual ICollection<UserConversation>? UserConversations { get; set; }
    public virtual ICollection<Message>? Messages { get; set; }
}

using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class User :EntityBase<Guid>, IDateTracking
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Fullname { get; set; }
    public int RoleId { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public string? CoverPhoto { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }
    public int Status { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<UserSocial>? UserSocials { get; set; }
    public virtual ICollection<Friend>? Friends { get; set; }
    public virtual ICollection<Notification>? Notifications { get; set; }
    public virtual ICollection<Conversation>? Conversations { get; set; }
    public virtual ICollection<UserConversation>? UserConversations { get; set; }
    public virtual ICollection<Message>? Messages { get; set; }
}

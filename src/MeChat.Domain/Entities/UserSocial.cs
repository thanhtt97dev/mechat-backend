using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class UserSocial : Entity, IDateTracking
{
    public Guid UserId { get; set; }    
    public int SocialId { get; set; }
    public string AccountSocialId { set; get; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }

    public virtual User? User { get; set; }
    public virtual Social? Social { get; set; }
}

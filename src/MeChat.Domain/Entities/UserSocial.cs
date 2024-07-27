using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class UserSocial : EntityAudit
{
    public Guid UserId { get; set; }    
    public int SocialId { get; set; }
    public string AccountSocialId { set; get; } = string.Empty;

    public virtual User? User { get; set; }
    public virtual Social? Social { get; set; }
}

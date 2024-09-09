using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class User : EntityAuditBase<Guid>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Fullname { get; set; }
    public int RoleId { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public int Status { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<UserSocial>? UserSocials { get; set; }
}

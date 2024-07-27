using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class Role : EntityAuditBase<int>
{
    public string? RoleName { get; set; }
    
    public virtual ICollection<User> Users { get; } = new List<User>();
}

using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class Role : DomainEntity<Guid>
{
    public int RoleName { get; set; }
    
    public virtual ICollection<User> Users { get; } = new List<User>();
}

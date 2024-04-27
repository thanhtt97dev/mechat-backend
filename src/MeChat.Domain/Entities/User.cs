using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class User : DomainEntity<Guid>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid RoldeId { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public int Status { get; set; }

    public virtual Role? Role { get; set; }
}

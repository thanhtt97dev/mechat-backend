using MeChat.Domain.Abstractions;
using System.Xml.Linq;

namespace MeChat.Domain.Entities;
public class User : DomainEntity<Guid>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

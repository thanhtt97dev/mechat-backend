using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class FriendStatus : EntityBase<int>
{
    public required string Name { get; set; }

    public virtual ICollection<Friend> Friends { get; } = new List<Friend>();
}

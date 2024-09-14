using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class Friend : Entity, IDateTracking
{
    public Guid UserFirstId { get; set; }
    public Guid UserSecondId { get; set; }
    public Guid SpecifierId { get; set; }
    public int Status { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }

    public virtual User? UserFirst { get; set; }
    public virtual User? UserSecond { get; set; }
    public virtual User? Specifier { get; set; }
    public virtual FriendStatus? FriendStatus { get; set; }
}

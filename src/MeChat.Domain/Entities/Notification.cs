using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class Notification : EntityBase<Guid>, IDateTracking
{
    public Guid UserId { get;set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }
    public bool IsReaded { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }

    public virtual User? User { get; set; }
}

using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class Notification : IEntityBase<Guid>, IDateTracking
{
    public Guid Id { get;set; }
    public Guid UserId { get;set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public bool IsReaded { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }

    public virtual User? User { get; set; }
}

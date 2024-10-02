using MeChat.Domain.Abstractions;
using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Entities;
public class Notification : EntityBase<Guid>
{
    public Guid UserId { get;set; }
    public DateTime CreatedDate { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }
    public bool IsReaded { get; set; }

    public virtual User? User { get; set; }
}

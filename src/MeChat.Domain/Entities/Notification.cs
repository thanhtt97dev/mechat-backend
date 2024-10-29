using MeChat.Domain.Abstractions;

namespace MeChat.Domain.Entities;
public class Notification : EntityBase<Guid>
{
    public Guid RequesterId { get; set; }
    public Guid ReceiverId { get;set; }
    public DateTime CreatedDate { get; set; }
    public int Type { get; set; } 
    public bool IsReaded { get; set; }

    public virtual User? Receiver { get; set; }
    public virtual User? Requester { get; set; }
}

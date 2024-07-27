using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Abstractions;
public abstract class EntityAudit : Entity, IAuditTable
{
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiledDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
}

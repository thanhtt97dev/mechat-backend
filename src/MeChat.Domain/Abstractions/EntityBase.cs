using MeChat.Domain.Abstractions.Enitites;

namespace MeChat.Domain.Abstractions;
public abstract class EntityBase<TKey> : Entity, IEntityBase<TKey>
{
    public TKey Id { get; set; }
}

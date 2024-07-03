namespace MeChat.Domain.Abstractions;
public class DomainEntity<TKey> : EntityMultiplePrimaryKey
{
    public virtual TKey Id { get; set; }
}

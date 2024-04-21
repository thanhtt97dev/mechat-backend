namespace MeChat.Domain.Abstractions.Entities;
public class DomainEntity<TKey>
{
    public virtual TKey Id { get; set; }
}

namespace MeChat.Domain.Abstractions;
public class DomainEntity<TKey>
{
    public virtual required TKey Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}

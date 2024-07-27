namespace MeChat.Domain.Abstractions.Enitites;
public interface IEntityBase<TKey>
{
    TKey Id { get; }
}

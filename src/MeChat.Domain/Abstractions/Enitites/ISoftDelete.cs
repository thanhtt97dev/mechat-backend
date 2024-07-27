namespace MeChat.Domain.Abstractions.Enitites;
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeleteAt { get; set; }

    public void Undo()
    {
        IsDeleted = false;
        DeleteAt = null;
    }
}

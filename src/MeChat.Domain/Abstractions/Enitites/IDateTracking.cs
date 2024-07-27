namespace MeChat.Domain.Abstractions.Enitites;
public interface IDateTracking
{
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset? ModifiledDate { get; set; }
}

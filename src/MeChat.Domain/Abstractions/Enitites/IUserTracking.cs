namespace MeChat.Domain.Abstractions.Enitites;
public interface IUserTracking
{
    Guid CreatedBy { get; set; }

    Guid? ModifiedBy { get; set; }
}

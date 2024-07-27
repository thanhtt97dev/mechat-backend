namespace MeChat.Domain.Abstractions.Enitites;
public interface IAuditTable : IDateTracking, IUserTracking, ISoftDelete
{
}

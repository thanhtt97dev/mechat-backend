namespace MeChat.Common.Abstractions.RealTime;
public interface IRealTimeContext<T>
{
    Task SendMessageAsync(string method, IReadOnlyList<string> connectionIds, string message);
    Task SendMessageAsync(string method, Guid userId, string message);
}

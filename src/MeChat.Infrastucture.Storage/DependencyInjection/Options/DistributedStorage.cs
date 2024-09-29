using MeChat.Infrastucture.Storage.DependencyInjection.Options;

namespace MeChat.Infrastucture.Storage.DependencyInjection.Options;
public class DistributedStorage
{
    public AwsS3 AwsS3 { get; set; } = new();
}

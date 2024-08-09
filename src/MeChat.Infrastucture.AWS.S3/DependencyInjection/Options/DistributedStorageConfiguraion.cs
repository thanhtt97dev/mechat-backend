using MeChat.Infrastucture.AWS.S3.DependencyInjection.Options;

namespace MeChat.Infrastucture.Storage.DependencyInjection.Options;
public class DistributedStorageConfiguraion
{
    public AwsS3Configuration AwsS3Configuration { get; set; } = new AwsS3Configuration();
}

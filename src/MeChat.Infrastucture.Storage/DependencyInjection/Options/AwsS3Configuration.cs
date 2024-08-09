namespace MeChat.Infrastucture.Storage.DependencyInjection.Options;
public class AwsS3Configuration
{
    public string? BucketName { get; set; }
    public string? Region { get; set; }
    public string? AwsAccessKeyId { get; set; }
    public string? AwsSecretAccessKey { get; set; }
    public string? AwsSessionToken { get; set; }
}

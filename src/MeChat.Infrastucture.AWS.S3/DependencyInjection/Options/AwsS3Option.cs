namespace MeChat.Infrastucture.AWS.S3.DependencyInjection.Options;
public class AwsS3Option
{
    public string? BucketName { get; set; }
    public string? Region { get; set; }
    public string? AwsAccessKeyId { get; set; }
    public string? AwsSecretAccessKey { get; set; }
    public string? AwsSessionToken { get; set; }
}

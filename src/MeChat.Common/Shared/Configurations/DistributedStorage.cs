namespace MeChat.Common.Shared.Configurations;
public sealed class DistributedStorage
{
    public string Mode { get; set; } = nameof(DistributedStorage.AwsS3);
    public AwsS3 AwsS3 { get; set; } = new();
    public AzureBlobStorage AzureBlobStorage { get; set;} = new();
}

public sealed class AwsS3
{
    public string? BucketName { get; set; }
    public string? Region { get; set; }
    public string? AwsAccessKeyId { get; set; }
    public string? AwsSecretAccessKey { get; set; }
    public string? AwsSessionToken { get; set; }
    public string? Endpoint { get; set; }
}

public sealed class AzureBlobStorage
{

}
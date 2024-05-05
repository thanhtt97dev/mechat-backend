using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Exceptions;
using MeChat.Infrastucture.AWS.S3.DependencyInjection.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MeChat.Infrastucture.AWS.S3.Services;
public class AwsS3Service : IStorageService
{
    private readonly AwsS3Option awsS3Option = new();
    private readonly IAmazonS3 awsS3Client;

    public AwsS3Service(IConfiguration configuration)
    {
        configuration.GetSection(nameof(AwsS3Option)).Bind(awsS3Option);
        this.awsS3Client = new AmazonS3Client(
            awsAccessKeyId: awsS3Option.AwsAccessKeyId,
            awsSecretAccessKey: awsS3Option.AwsSecretAccessKey,
            region: RegionEndpoint.GetBySystemName(awsS3Option.Region));
    }

    public async Task DeleteFileAsync(string filename, string? versionId = "")
    {
        DeleteObjectRequest request = new DeleteObjectRequest()
        {
            BucketName = awsS3Option.BucketName,
            Key = filename
        };

        if (!string.IsNullOrEmpty(versionId))
            request.VersionId = versionId;

        await awsS3Client.DeleteObjectAsync(request);
    }

    public async Task<byte[]> DownloadFileAsync(string file)
    {
        MemoryStream? memoryStream = null;

        GetObjectRequest getObjectRequest = new GetObjectRequest()
        {
            BucketName = awsS3Option.BucketName,
            Key = file,
        };

        using (var response = await awsS3Client.GetObjectAsync(getObjectRequest))
        {
            if(response.HttpStatusCode == System.Net.HttpStatusCode.OK) 
            {
                using(memoryStream = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(memoryStream);
                }
            }
        }

        if (memoryStream == null || memoryStream.ToArray().Length < 1)
            throw new AwsS3Exceptions.NotFound(file);

        return memoryStream.ToArray();
    }

    public async Task<bool> UploadFileAsync(IFormFile file, string fileName)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = memoryStream,
                Key = fileName,
                BucketName = awsS3Option.BucketName,
                ContentType = file.ContentType
            };

            var fileTransferUtility = new TransferUtility(awsS3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return true;
        }
    }
}

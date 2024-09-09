using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MeChat.Common.Abstractions.Services;
using MeChat.Common.Shared.Exceptions;
using MeChat.Infrastucture.Storage.DependencyInjection.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MeChat.Infrastucture.Storage.Services;
public class AwsS3Service : IStorageService
{
    private readonly DistributedStorageConfiguraion distributedStorage = new();
    private readonly IAmazonS3 awsS3Client;

    public AwsS3Service(IConfiguration configuration)
    {
        configuration.GetSection(nameof(DistributedStorageConfiguraion)).Bind(distributedStorage);
        this.awsS3Client = new AmazonS3Client(
            awsAccessKeyId: distributedStorage.AwsS3Configuration.AwsAccessKeyId,
            awsSecretAccessKey: distributedStorage.AwsS3Configuration.AwsSecretAccessKey,
            region: RegionEndpoint.GetBySystemName(distributedStorage.AwsS3Configuration.Region));
    }

    public async Task DeleteFileAsync(string filename, string? versionId = "")
    {
        DeleteObjectRequest request = new DeleteObjectRequest()
        {
            BucketName = distributedStorage.AwsS3Configuration.BucketName,
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
            BucketName = distributedStorage.AwsS3Configuration.BucketName,
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

    public async Task<string> UploadFileAsync(IFormFile file, string fileName)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = memoryStream,
                Key = fileName,
                BucketName = distributedStorage.AwsS3Configuration.BucketName,
                ContentType = file.ContentType
            };

            var fileTransferUtility = new TransferUtility(awsS3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            var endpoint = distributedStorage.AwsS3Configuration.Endpoint + fileName;

            return endpoint;
        }
    }
}

using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Storage.DependencyInjection.Extentions;
public static class StorageExtention
{

    #region Add Storage
    public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        Common.Shared.Configurations.DistributedStorage distributedStorageConfig = new();
        configuration.GetSection(nameof(Common.Shared.Configurations.DistributedStorage)).Bind(distributedStorageConfig);

        switch(distributedStorageConfig.Mode) 
        {
            case nameof(Common.Shared.Configurations.DistributedStorage.AwsS3):
                services.AddAmazonS3();
                break;
            case nameof(Common.Shared.Configurations.DistributedStorage.AzureBlobStorage):
                break;
            default:
                services.AddAmazonS3();
                break;
        }
    }
    #endregion

    #region Amazon S3
    public static void AddAmazonS3(this IServiceCollection services)
    {
        services.AddTransient<IStorageService, AwsS3Service>();
    }
    #endregion

    #region Azure Blob Storage
    public static void AddAzureBlobStorage(this IServiceCollection services)
    {
    }
    #endregion
}

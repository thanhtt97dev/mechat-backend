using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.AWS.S3.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.AWS.S3.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddAmazonS3(this IServiceCollection services)
    {
        services.AddTransient<IStorageService, AwsS3Service>();
    }
}

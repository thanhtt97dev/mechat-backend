using MeChat.Common.Abstractions.Services;
using MeChat.Infrastucture.Storage.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.Storage.DependencyInjection.Extentions;
public static class ServiceCollectionExtentions
{
    public static void AddAmazonS3(this IServiceCollection services)
    {
        services.AddTransient<IStorageService, AwsS3Service>();
    }
}

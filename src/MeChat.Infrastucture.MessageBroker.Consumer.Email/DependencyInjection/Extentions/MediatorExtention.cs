using Microsoft.Extensions.DependencyInjection;

namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MediatorExtention
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });
    }
}

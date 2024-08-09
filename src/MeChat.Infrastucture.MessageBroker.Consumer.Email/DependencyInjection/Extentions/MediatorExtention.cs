namespace MeChat.Infrastucture.MessageBroker.Consumer.Email.DependencyInjection.Extentions;

public static class MediatorExtention
{
    public static void AddConfigurationMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configs =>
        {
            configs.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });
    }
}

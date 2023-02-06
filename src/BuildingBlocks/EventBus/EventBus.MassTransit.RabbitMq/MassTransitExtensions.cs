using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.MassTransit.RabbitMq
{
    public static class MassTransitExtensions
    {
        public static void AddMassTransitAsEventBus(
            this IServiceCollection services,
            Action<IServiceCollectionBusConfigurator> busConfigurator,
            Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext> factoryConfigurator)
        {
            services.AddMassTransit(cfg =>
            {
                busConfigurator.Invoke(cfg);

                cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(factory =>
                {
                    factory.ConfigureEndpoints(provider);

                    factory.Host(Global.Config.Uri, h =>
                    {
                        h.Username(Global.Config.UserName);
                        h.Password(Global.Config.Password);
                    });

                    factoryConfigurator.Invoke(factory, provider);
                }));
            });
        }
    }
}

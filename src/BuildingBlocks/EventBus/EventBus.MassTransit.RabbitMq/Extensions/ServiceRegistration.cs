using EventBus.MassTransit.RabbitMq.Configurations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.MassTransit.RabbitMq.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureMassTransit(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<IBusRegistrationContext, IBusRegistrationConfigurator, IRabbitMqBusFactoryConfigurator> registrationAction)
        {
            RabbitMqSettings settings = (RabbitMqSettings)configuration.GetSection("RabbitMqSettings");

            services.AddMassTransit(opt =>
            {
                opt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.RabbitMQUri, hostOpt =>
                    {
                        hostOpt.Username(settings.RabbitMQUserName);
                        hostOpt.Password(settings.RabbitMQPassword);
                    });
                    registrationAction(context, opt, cfg);
                });
            });

            return services;
        }
    }
}

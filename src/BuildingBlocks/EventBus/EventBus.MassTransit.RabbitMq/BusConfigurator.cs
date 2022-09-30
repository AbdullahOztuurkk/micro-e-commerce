using EventBus.MassTransit.RabbitMq.Configurations;
using MassTransit;

namespace EventBus.MassTransit.RabbitMq
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq((configuration) =>
            {
                configuration.Host(RabbitMqConstants.RabbitMQUri, hostOpt =>
                {
                    hostOpt.Username(RabbitMqConstants.RabbitMQUserName);
                    hostOpt.Password(RabbitMqConstants.RabbitMQPassword);
                });
                registrationAction?.Invoke(configuration);
            });
        }
    }
}

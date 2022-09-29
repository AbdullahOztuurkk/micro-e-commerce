using MassTransit;

namespace EventBus.MassTransit.RabbitMq.Events.Handlers
{
    public interface IIntegrationEventHandler<TEvent> : IConsumer<TEvent> where TEvent : IntegrationEvent
    {

    }
}

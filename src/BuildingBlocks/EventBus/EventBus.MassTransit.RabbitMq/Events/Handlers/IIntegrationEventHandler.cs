namespace EventBus.MassTransit.RabbitMq.Events.Handlers
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}

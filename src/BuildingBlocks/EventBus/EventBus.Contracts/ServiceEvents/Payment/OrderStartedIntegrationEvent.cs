using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.ServiceEvents.Payment
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }

        public OrderStartedIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public OrderStartedIntegrationEvent() { }
    }
}

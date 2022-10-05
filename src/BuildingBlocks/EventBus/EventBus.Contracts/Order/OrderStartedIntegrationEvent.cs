using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.Order
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public OrderStartedIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }

        public OrderStartedIntegrationEvent() { }
    }
}

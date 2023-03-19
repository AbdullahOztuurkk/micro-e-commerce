using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.ServiceEvents.Notification
{
    public class OrderPaymentSuccessIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public OrderPaymentSuccessIntegrationEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}

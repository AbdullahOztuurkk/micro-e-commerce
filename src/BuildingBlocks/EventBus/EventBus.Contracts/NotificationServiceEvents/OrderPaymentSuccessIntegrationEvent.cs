using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.NotificationServiceEvents
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

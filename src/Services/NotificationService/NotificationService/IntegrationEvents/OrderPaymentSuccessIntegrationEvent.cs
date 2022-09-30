using EventBus.MassTransit.RabbitMq.Events;

namespace NotificationService.IntegrationEvents
{
    public class OrderPaymentSuccessIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public OrderPaymentSuccessIntegrationEvent(Guid orderId) => OrderId = orderId;
    }
}

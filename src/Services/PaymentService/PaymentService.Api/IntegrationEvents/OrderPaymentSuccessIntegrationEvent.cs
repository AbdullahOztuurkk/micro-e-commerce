using EventBus.MassTransit.RabbitMq.Events;

namespace PaymentService.Api.IntegrationEvents
{
    public class OrderPaymentSuccessIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public OrderPaymentSuccessIntegrationEvent(Guid orderId) => OrderId = orderId;
    }
}

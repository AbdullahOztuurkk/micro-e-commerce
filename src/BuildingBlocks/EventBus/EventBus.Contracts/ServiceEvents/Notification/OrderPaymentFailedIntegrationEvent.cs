using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.ServiceEvents.Notification
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }

        public string ErrorMessage { get; }

        public OrderPaymentFailedIntegrationEvent(Guid orderId, string errorMessage)
        {
            OrderId = orderId;
            ErrorMessage = errorMessage;
        }
    }
}

namespace EventBus.MassTransit.RabbitMq
{
    public class Global
    {
        public static class Config
        {
            public const string Uri = "rabbitmq://localhost/";
            public const string UserName = "guest";
            public const string Password = "guest";
        }

        public static class Queues
        {
            public const string OrderStartedIntegrationEvent            = "EventBus.Contracts.Order.OrderStartedIntegrationEvent";
            public const string OrderPaymentSuccessIntegrationEvent     = "EventBus.Contracts.Order.OrderPaymentSuccessIntegrationEvent";
            public const string OrderPaymentFailedIntegrationEvent      = "EventBus.Contracts.Order.OrderPaymentFailedIntegrationEvent";
            public const string OrderCreatedIntegrationEvent            = "EventBus.Contracts.Order.OrderCreatedIntegrationEvent";
        }
    }
}

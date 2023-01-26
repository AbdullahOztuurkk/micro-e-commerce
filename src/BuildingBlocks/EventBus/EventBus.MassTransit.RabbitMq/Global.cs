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

        public static class Services
        {
            public const string OrderServiceQueueName = "order.service";
            public const string NotificationServiceQueueName = "notification.service";
            public const string PaymentServiceQueueName = "payment.service";
            public const string CatalogServiceQueueName = "catalog.service";
            public const string BasketServiceQueueName = "basket.service";
        }
    }
}

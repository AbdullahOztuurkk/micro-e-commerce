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
            public static class NotificationService
            {
                public const string OrderPaymentSuccessIntegrationEvent = "NotificationService.Order-Payment-Success";
                public const string OrderPaymentFailedIntegrationEvent  = "NotificationService.Order-Payment-Failed";
            }

            public static class PaymentService
            {
                public const string OrderStartedIntegrationEvent = "PaymentService.Order-Started";
            }

            public static class BasketService
            {
                public const string OrderCreatedIntegrationEvent = "BasketService.Order-Created";
            }
        }
    }
}

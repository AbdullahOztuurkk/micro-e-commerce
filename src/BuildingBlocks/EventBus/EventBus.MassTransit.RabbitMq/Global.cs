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
            public const string Notification_OrderPaymentSuccess = "NotificationService.Order-Payment-Success";
            public const string Notification_OrderPaymentFailed = "NotificationService.Order-Payment-Failed";
            public const string Payment_OrderStarted = "PaymentService.Order-Started";
            public const string Basket_OrderCreated = "BasketService.Order-Created";
            public const string Order_OrderStarted = "OrderService.Order-Started";
        }
    }
}

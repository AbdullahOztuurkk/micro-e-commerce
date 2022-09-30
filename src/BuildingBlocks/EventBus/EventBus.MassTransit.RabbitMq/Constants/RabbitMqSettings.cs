using Microsoft.Extensions.Configuration;

namespace EventBus.MassTransit.RabbitMq.Configurations
{
    public static class RabbitMqConstants
    {
        public const string RabbitMQUri = "rabbitmq://localhost/";
        public const string RabbitMQUserName  = "guest";
        public const string RabbitMQPassword = "guest";
    }
}
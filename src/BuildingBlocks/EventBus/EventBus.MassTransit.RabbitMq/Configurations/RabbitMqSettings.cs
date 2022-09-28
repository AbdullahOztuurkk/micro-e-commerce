using Microsoft.Extensions.Configuration;

namespace EventBus.MassTransit.RabbitMq.Configurations
{
    public class RabbitMqSettings
    {
        public string RabbitMQUri { get; set; }
        public string RabbitMQUserName { get; set; }
        public string RabbitMQPassword { get; set; }
    }
}
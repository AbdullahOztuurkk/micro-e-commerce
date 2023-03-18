using EventBus.Contracts.NotificationServiceEvents;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.Handlers
{
    public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly ILogger<OrderPaymentFailedIntegrationEventHandler> logger;

        public OrderPaymentFailedIntegrationEventHandler(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<OrderPaymentFailedIntegrationEventHandler>();
        }

        public Task Consume(ConsumeContext<OrderPaymentFailedIntegrationEvent> context)
        {
            //Fake email logic
            logger.LogInformation($"Order payment has been failed! Order Id: {context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}

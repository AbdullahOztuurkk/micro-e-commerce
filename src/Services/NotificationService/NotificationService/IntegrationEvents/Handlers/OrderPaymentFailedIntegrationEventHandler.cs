using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.IntegrationEvents.Handlers
{
    public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly ILogger<OrderPaymentFailedIntegrationEventHandler> logger;

        public OrderPaymentFailedIntegrationEventHandler(ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<OrderPaymentFailedIntegrationEvent> context)
        {
            //Fake email logic
            logger.LogInformation($"Order payment has been failed! Order Id: {context.Message.OrderId}");
            return Task.CompletedTask;
        }
    }
}

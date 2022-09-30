using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.IntegrationEvents.Handlers
{
    public class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        private readonly ILogger<OrderPaymentSuccessIntegrationEventHandler> logger;

        public OrderPaymentSuccessIntegrationEventHandler(ILogger<OrderPaymentSuccessIntegrationEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<OrderPaymentSuccessIntegrationEvent> context)
        {
            //Fake email logic
            logger.LogInformation("Order payment has been succeeded!");
            return Task.CompletedTask;
        }
    }
}

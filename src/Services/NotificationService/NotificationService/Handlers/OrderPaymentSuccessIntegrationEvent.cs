using EventBus.Contracts.Order;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.Handlers
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
            var message = context.Message;
            Console.Out.WriteLine($"Order payment has been succeeded with Id : {message.Id}!");
            return Task.CompletedTask;
        }
    }
}

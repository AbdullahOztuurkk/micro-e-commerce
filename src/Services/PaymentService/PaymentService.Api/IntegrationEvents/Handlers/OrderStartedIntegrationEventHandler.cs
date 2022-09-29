﻿using EventBus.MassTransit.RabbitMq.Events;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;

namespace PaymentService.Api.IntegrationEvents.Handlers
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IConfiguration configuration;
        private readonly IBus bus;
        private readonly ILogger<OrderStartedIntegrationEventHandler> logger;

        public OrderStartedIntegrationEventHandler(IConfiguration configuration, IBus bus, ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            this.configuration = configuration;
            this.bus = bus;
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<OrderStartedIntegrationEvent> context)
        {
            string keyword = "PaymentSuccess";
            bool PaymentSuccessFlag = configuration.GetValue<bool>(keyword);

            IntegrationEvent paymentEvent = PaymentSuccessFlag
                ? new OrderPaymentSuccessIntegrationEvent(context.Message.Id)
                : new OrderPaymentFailedIntegrationEvent(context.Message.Id, $"Order has been failed due of Order Id: {context.Message.Id}");

            bus.Publish(paymentEvent);

            return Task.CompletedTask;
        }
    }
}
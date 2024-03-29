﻿using EventBus.Contracts.ServiceEvents.Notification;
using EventBus.Contracts.ServiceEvents.Payment;
using EventBus.MassTransit.RabbitMq.Events;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;

namespace PaymentService.Api.Handlers
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<OrderStartedIntegrationEventHandler> logger;

        public OrderStartedIntegrationEventHandler(IConfiguration configuration, ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<OrderStartedIntegrationEvent> context)
        {
            string keyword = "PaymentSuccess";
            bool PaymentSuccessFlag = configuration.GetValue<bool>(keyword);

            logger.LogInformation($"Order started event has been triggered with order id: {context.Message.OrderId}");

            IntegrationEvent paymentEvent = PaymentSuccessFlag
                ? new OrderPaymentSuccessIntegrationEvent(context.Message.Id)
                : new OrderPaymentFailedIntegrationEvent(context.Message.Id, $"Order has been failed due of Order Id: {context.Message.Id}");

            context.Publish(paymentEvent);

            return Task.CompletedTask;
        }
    }
}

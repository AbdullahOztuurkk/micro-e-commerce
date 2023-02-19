using BasketService.Application.Repository;
using EventBus.Contracts.Order;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;

namespace BasketService.Api.Handlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IBasketRepository repository;
        private readonly ILogger<OrderCreatedIntegrationEventHandler> logger;

        public OrderCreatedIntegrationEventHandler(IBasketRepository repository, ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            var data = context.Message;
            logger.LogInformation($"Handling Integration Event: {data.Id} at BasketService.Api - { data.GetType().Name } ");
            await repository.DeleteBasketAsync(data.UserId);
        }
    }
}

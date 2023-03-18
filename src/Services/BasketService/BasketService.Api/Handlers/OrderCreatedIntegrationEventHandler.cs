using BasketService.Application.Repository;
using EventBus.Contracts.BasketServiceEvents;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;

namespace BasketService.Api.Handlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IBasketRepository repository;
        private readonly ILogger<OrderCreatedIntegrationEventHandler> logger;

        public OrderCreatedIntegrationEventHandler(IBasketRepository repository, ILoggerFactory loggerFactory)
        {
            this.repository = repository;
            this.logger = loggerFactory.CreateLogger<OrderCreatedIntegrationEventHandler>();
        }

        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            var data = context.Message;
            logger.LogInformation($"Handling Integration Event: {data.Id} at BasketService.Api - { data.GetType().Name } ");
            await repository.DeleteBasketAsync(data.UserId);
        }
    }
}

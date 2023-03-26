using EventBus.Contracts.ServiceEvents.Order;
using EventBus.MassTransit.RabbitMq.Events.Handlers;
using MassTransit;
using MediatR;
using OrderService.Application.Features.Commands.CreateOrder;
using OrderService.Domain.Models;

namespace OrderService.Api.IntegrationEventHandlers
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;
        private readonly IMediator mediator;
        public OrderStartedIntegrationEventHandler(ILogger<OrderStartedIntegrationEventHandler> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }
        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            var orderCreatedObj = context.Message;

            try
            {
                _logger.LogInformation("Handling integration event at {AppName} - ({@IntegrationEvent})",
                    GetType().Namespace,
                    orderCreatedObj.GetType().Name);

                var createOrderCommand = new CreateOrderCommand(
                    orderCreatedObj.Basket.Items.Select(x => new BasketItem
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        OldUnitPrice = x.OldUnitPrice,
                        UnitPrice = x.UnitPrice,
                        ProductName = x.ProductName,
                        Quantity = x.Quantity
                    }).ToList(),
                    orderCreatedObj.Buyer,
                    orderCreatedObj.UserName,
                    orderCreatedObj.Address.City,
                    orderCreatedObj.Address.Street,
                    orderCreatedObj.Address.State,
                    orderCreatedObj.Address.Country,
                    orderCreatedObj.Address.ZipCode,
                    orderCreatedObj.CardInformation.CardNumber,
                    orderCreatedObj.CardInformation.CardHolderName,
                    orderCreatedObj.CardInformation.CardExpiration,
                    orderCreatedObj.CardInformation.CardSecurityNumber,
                    orderCreatedObj.CardInformation.CardTypeId);

                await mediator.Send(createOrderCommand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
        }
    }
}

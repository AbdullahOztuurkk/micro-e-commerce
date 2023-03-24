using MediatR;
using OrderService.Application.Contracts.Repositories;
using OrderService.Domain.AggregateModels.BuyerAggregate;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandlers
{
    public class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IBuyerRepository buyerRepository;

        public OrderStartedDomainEventHandler(IBuyerRepository buyerRepository)
        {
            this.buyerRepository = buyerRepository;
        }

        public async Task Handle(OrderStartedDomainEvent @event, CancellationToken cancellationToken)
        {
            var cardTypeId = (@event.CardTypeId != 0) ? @event.CardTypeId : 1;

            var buyer = await buyerRepository.GetSingleAsync(i => i.Name == @event.UserName, i => i.PaymentMethods);

            bool buyerOriginallyExisted = buyer != null;

            if (!buyerOriginallyExisted)
            {
                buyer = new Buyer(@event.UserName);
            }

            buyer.VerifyOrAddPaymentMethod(cardTypeId,
                                            $"Payment Method on {DateTime.UtcNow}",
                                            @event.CardNumber,
                                            @event.CardSecurityNumber,
                                            @event.CardHolder,
                                            @event.CardExpiration,
                                            @event.Order.Id);

            var buyerUpdated = buyerOriginallyExisted
                ? buyerRepository.Update(buyer)
                : await buyerRepository.AddAsync(buyer);

            await buyerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}

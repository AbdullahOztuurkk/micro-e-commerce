using MediatR;
using OrderService.Application.Contracts.Repositories;
using OrderService.Domain.Events;

namespace OrderService.Application.DomainEventHandlers
{
    public class UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler : INotificationHandler<BuyerAndPaymentMethodVerifiedDomainEvent>
    {
        private readonly IOrderRepository orderRepository;

        public UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task Handle(BuyerAndPaymentMethodVerifiedDomainEvent @event, CancellationToken cancellationToken)
        {
            var orderToUpdate = await orderRepository.GetByIdAsync(@event.OrderId);
            orderToUpdate.SetBuyerId(@event.Buyer.Id);
            orderToUpdate.SetPaymentMethodId(@event.PaymentMethod.Id);
        }
    }
}

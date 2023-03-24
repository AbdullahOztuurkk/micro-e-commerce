using EventBus.Contracts.ServiceEvents.Payment;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Contracts.Repositories;
using OrderService.Domain.AggrementModels.OrderAggregate;
using OrderService.Domain.Models;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<bool>
    {

        private readonly List<OrderItemDTO> _orderItems;



        public string UserName { get; private set; }


        public string City { get; private set; }


        public string Street { get; private set; }


        public string State { get; private set; }


        public string Country { get; private set; }


        public string ZipCode { get; private set; }


        public string CardNumber { get; private set; }


        public string CardHolderName { get; private set; }


        public DateTime CardExpiration { get; private set; }


        public string CardSecurityNumber { get; private set; }


        public int CardTypeId { get; private set; }


        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;

        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDTO>();
        }


        public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName, string city, string street, string state, string country, string zipcode,
            string cardNumber, string cardHolderName, DateTime cardExpiration,
            string cardSecurityNumber, int cardTypeId) : this()
        {
            var dtoList = basketItems.Select(item => new OrderItemDTO()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            });

            _orderItems = dtoList.ToList();

            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            CardExpiration = cardExpiration;
        }
    }

    public class OrderItemDTO
    {
        public int ProductId { get; init; }

        public string ProductName { get; init; }

        public decimal UnitPrice { get; init; }

        public int Units { get; init; }

    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IBus eventBus;
        private readonly ILogger<CreateOrderCommandHandler> logger;


        public CreateOrderCommandHandler(IOrderRepository orderRepository, IBus eventBus, ILoggerFactory loggerFactory)
        {
            this.orderRepository = orderRepository;
            this.eventBus = eventBus;
            this.logger = loggerFactory.CreateLogger<CreateOrderCommandHandler>();
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateOrderCommandHandler -> Handle method invoked");

            var addr = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);

            Order dbOrder = new(request.UserName,
                addr, request.CardTypeId, request.CardNumber, request.CardSecurityNumber, request.CardHolderName, request.CardExpiration, null);

            foreach (var orderItem in request.OrderItems)
            {
                dbOrder.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice, orderItem.Units);
            }

            await orderRepository.AddAsync(dbOrder);
            await orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            logger.LogInformation("CreateOrderCommandHandler -> dbOrder saved");

            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(dbOrder.Id);

            eventBus.Publish(orderStartedIntegrationEvent);

            logger.LogInformation("CreateOrderCommandHandler -> Payment_OrderStarted fired");

            return true;
        }
    }
}

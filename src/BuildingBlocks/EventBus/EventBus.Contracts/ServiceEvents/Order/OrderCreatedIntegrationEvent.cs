using EventBus.Contracts.Models;
using EventBus.Contracts.Models.Basket;
using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.ServiceEvents.Order
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderNumber { get; set; }
        public Address Address { get; set; }
        public CardInformation CardInformation { get; set; }
        public string Buyer { get; set; }
        public CustomerBasket Basket { get; }

        public OrderCreatedIntegrationEvent(CustomerBasket basket,string userId, string userName, int orderNumber, Address address, CardInformation cardInformation, string buyer)
        {
            Basket = basket;
            UserId = userId;
            UserName = userName;
            OrderNumber = orderNumber;
            Address = address;
            CardInformation = cardInformation;
            Buyer = buyer;
        }

    }
}

using EventBus.Contracts.Models;
using EventBus.MassTransit.RabbitMq.Events;

namespace EventBus.Contracts.BasketServiceEvents
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int OrderNumber { get; set; }
        public Address Address { get; set; }
        public CardInformation CardInformation { get; set; }
        public string Buyer { get; set; }
        public OrderCreatedIntegrationEvent(string userId, string userName, int orderNumber, Address address, CardInformation cardInformation, string buyer)
        {
            UserId = userId;
            UserName = userName;
            OrderNumber = orderNumber;
            Address = address;
            CardInformation = cardInformation;
            Buyer = buyer;
        }
    }
}

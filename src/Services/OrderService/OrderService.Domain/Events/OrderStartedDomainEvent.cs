using MediatR;
using OrderService.Domain.AggrementModels.OrderAggregate;

namespace OrderService.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string UserName { get; set; }
        public int CardTypeId { get; set; }
        public string CardNumber { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime CardExpiration { get; set; }
        public Order Order { get; set; }

        public OrderStartedDomainEvent(Order order, string userName, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolder, DateTime cardExpiration)
        {
            Order = order;
            UserName = userName;
            CardTypeId = cardTypeId;
            CardNumber = cardNumber;
            CardSecurityNumber = cardSecurityNumber;
            CardHolder = cardHolder;
            CardExpiration = cardExpiration;
        }
    }
}

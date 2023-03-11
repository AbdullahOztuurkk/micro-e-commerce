using OrderService.Domain.SeedWork;

namespace OrderService.Domain.AggregateModels.BuyerAggregate
{
    public class PaymentMethod : BaseEntity
    {
        public string Alias { get; set; }
        public string CardNumber { get; set; }
        public string SecurityNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime Expiration { get; set; }

        public int CardTypeId { get; set; }


        public CardType CardType { get; private set; }


        public PaymentMethod() { }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            if (expiration < DateTime.UtcNow)
            {
                throw new Exception(nameof(expiration));
            }

            Alias = alias;
            Expiration = expiration;
            CardTypeId = cardTypeId;
            //CardType = CardType.FromValue<CardType>(cardTypeId);
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return CardTypeId == cardTypeId
                && CardNumber == cardNumber
                && Expiration == expiration;
        }
    }
}

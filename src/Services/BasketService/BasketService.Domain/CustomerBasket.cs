using System.ComponentModel.DataAnnotations;

namespace BasketService.Domain
{
    public class CustomerBasket : IValidatableObject
    {
        public string BuyerId { get; set; }
        public List<BasketItem> items { get; set; } = new List<BasketItem>();
        public CustomerBasket()
        {

        }

        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

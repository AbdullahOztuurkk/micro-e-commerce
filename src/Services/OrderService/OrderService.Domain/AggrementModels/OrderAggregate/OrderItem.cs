using OrderService.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.AggrementModels.OrderAggregate
{
    public class OrderItem : BaseEntity, IValidatableObject
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }

        public OrderItem(int productId, string productName, decimal unitPrice, int units)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Units = units;
        }

        public OrderItem()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Units <= 0)
            {
                results.Add(new ValidationResult("Invalid number of Units!", new[] { "Units" }));
            }

            return results;
        }
    }
}

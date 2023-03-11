using OrderService.Domain.SeedWork;

namespace OrderService.Domain.AggrementModels.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation = new(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed = new(3, nameof(StockConfirmed).ToLowerInvariant());
        public static OrderStatus Paid = new(4, nameof(Paid).ToLowerInvariant());
        public static OrderStatus Shipped = new(5, nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled = new(6, nameof(Cancelled).ToLowerInvariant());
        protected OrderStatus(int id, string name) : base(id, name)
        {

        }
        public static IEnumerable<OrderStatus> ToList() =>
            new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };
        public static OrderStatus FromName(string Name)
        {
            var state = ToList().SingleOrDefault(o => string.Equals(o.Name, Name, StringComparison.OrdinalIgnoreCase));

            return state ?? throw new ArgumentException($"Possible values for OrderStatus:{ string.Join(" , ", ToList().Select(x => x.Name)) } ");
        }

        public static OrderStatus From(int Id)
        {
            var state = ToList().SingleOrDefault(o => o.Id == Id) ;

            return state ?? throw new ArgumentException($"Possible values for OrderStatus:{string.Join(" , ", ToList().Select(x => x.Name))} ");
        }
    }
}

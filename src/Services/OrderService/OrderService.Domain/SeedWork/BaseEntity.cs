using MediatR;

namespace OrderService.Domain.SeedWork
{
    public class BaseEntity
    {
        public virtual Guid Id { get; protected set; }
        public DateTime CreatedDate { get; set; }

        public List<INotification> domainEvents { get; set; }
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification @event) => domainEvents = domainEvents ?? new List<INotification> { @event };
        public void RemoveDomainEvent(INotification @event) => domainEvents?.Remove(@event);
        public void ClearDomainEvents() => domainEvents?.Clear();

        public override bool Equals(object? obj)
        {
            if(obj is null || !(obj is BaseEntity)) return false;

            if(ReferenceEquals(this, obj)) return true;

            if (GetType() != obj.GetType())
                return false;

            BaseEntity item = (BaseEntity)obj;

            if (item.Id == default || Id == default)
                return false;
            else
                return item.Id == Id;
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            if (Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }
    }
}

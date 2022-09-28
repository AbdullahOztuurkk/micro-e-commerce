using System.Text.Json.Serialization;

namespace EventBus.MassTransit.RabbitMq.Events
{
    public class IntegrationEvent
    {

        [JsonPropertyName(nameof(Id))]
        public Guid Id { get; private set; }

        [JsonPropertyName(nameof(CreatedDate))]
        public DateTime CreatedDate { get; private set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }
    }
}

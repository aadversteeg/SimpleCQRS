using SimpleCQRS.Domain.Events;

namespace SimpleCQRS.Infrastructure.InMemory
{
    public interface IEventPublisher
    {
        void Publish(Event @event);
    }
}

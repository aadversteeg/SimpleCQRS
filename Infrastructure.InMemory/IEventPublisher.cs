using Domain.Events;

namespace Infrastructure.InMemory
{
    public interface IEventPublisher
    {
        void Publish(Event @event);
    }
}

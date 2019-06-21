using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IEventStore
    {
        Task SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}

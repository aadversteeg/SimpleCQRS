using SimpleCQRS.Application;
using SimpleCQRS.Domain.Events;
using System;
using System.Collections.Generic;

namespace SimpleCQRS.Infrastructure
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}

using System;

namespace SimpleCQRS.Domain.Events
{
    public class ItemsRemovedFromInventory : DomainEvent
    {
        public Guid Id;
        public readonly int Count;

        public ItemsRemovedFromInventory(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}

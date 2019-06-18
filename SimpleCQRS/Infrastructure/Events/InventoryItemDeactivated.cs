using System;

namespace SimpleCQRS.Infrastructure.Events
{
    public class InventoryItemDeactivated : Event
    {
        public readonly Guid Id;

        public InventoryItemDeactivated(Guid id)
        {
            Id = id;
        }
    }
}

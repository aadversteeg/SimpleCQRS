using System;

namespace SimpleCQRS.Domain.Events
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

using System;

namespace Domain.Events
{
    public class InventoryItemRenamed : DomainEvent
    {
        public readonly Guid Id;
        public readonly string NewName;

        public InventoryItemRenamed(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}

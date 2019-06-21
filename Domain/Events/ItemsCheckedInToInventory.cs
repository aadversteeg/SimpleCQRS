using System;

namespace Domain.Events
{
    public class ItemsCheckedInToInventory : DomainEvent
    {
        public Guid Id;
        public readonly int Count;

        public ItemsCheckedInToInventory(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}

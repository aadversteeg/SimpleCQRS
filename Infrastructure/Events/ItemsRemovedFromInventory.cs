﻿using System;

namespace Infrastructure.Events
{
    public class ItemsRemovedFromInventory : Event
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

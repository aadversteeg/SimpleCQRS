﻿using System;

namespace SimpleCQRS.Domain.Events
{
    public class InventoryItemCreated : Event
    {
        public readonly Guid Id;
        public readonly string Name;
        public InventoryItemCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

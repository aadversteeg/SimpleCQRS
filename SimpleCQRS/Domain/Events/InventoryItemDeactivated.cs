﻿using System;

namespace SimpleCQRS.Domain.Events
{
    public class InventoryItemDeactivated : DomainEvent
    {
        public readonly Guid Id;

        public InventoryItemDeactivated(Guid id)
        {
            Id = id;
        }
    }
}

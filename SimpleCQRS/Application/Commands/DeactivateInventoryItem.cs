﻿using System;

namespace SimpleCQRS.Application.Commands
{
    public class DeactivateInventoryItem : Command
    {
        public readonly Guid InventoryItemId;
        public readonly int OriginalVersion;

        public DeactivateInventoryItem(Guid inventoryItemId, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            OriginalVersion = originalVersion;
        }
    }
}

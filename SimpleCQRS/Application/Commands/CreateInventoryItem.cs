using System;
namespace SimpleCQRS.Application.Commands
{
    public class CreateInventoryItem : Command
    {
        public readonly Guid InventoryItemId;
        public readonly string Name;

        public CreateInventoryItem(Guid inventoryItemId, string name)
        {
            InventoryItemId = inventoryItemId;
            Name = name;
        }
    }
}

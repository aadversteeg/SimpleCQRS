using System;
using System.Collections.Generic;

namespace SimpleCQRS.Infrastructure.InMemory
{
    public static class BullShitDatabase
    {
        public static Dictionary<Guid, InventoryItemDetailsDto> details = new Dictionary<Guid, InventoryItemDetailsDto>();
        public static List<InventoryItemListDto> list = new List<InventoryItemListDto>();
    }
}

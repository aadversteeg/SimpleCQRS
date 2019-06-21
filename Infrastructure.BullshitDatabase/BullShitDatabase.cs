using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.BullshitDatabase
{
    public class BullShitDatabase : IDatabase
    {
        public static Dictionary<Guid, InventoryItemDetailsDto> details = new Dictionary<Guid, InventoryItemDetailsDto>();
        public static List<InventoryItemListDto> list = new List<InventoryItemListDto>();

        public IQueryable<InventoryItemListDto> ListItems => list.AsQueryable();

        public IQueryable<InventoryItemDetailsDto> DetailItems => details.Values.AsQueryable();

        public void Delete(InventoryItemListDto item)
        {
            list.Remove(item);
        }

        public void Delete(InventoryItemDetailsDto item)
        {
            details.Remove(item.Id);
       }

        public void Insert(InventoryItemListDto item)
        {
            list.Add(item);
        }

        public void Insert(InventoryItemDetailsDto item)
        {
            details.Add(item.Id, item);
        }
    }
}

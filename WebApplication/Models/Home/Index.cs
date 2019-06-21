using Infrastructure;
using System.Collections.Generic;

namespace WebApplication.Models.Home
{
    public class Index
    {
        public IEnumerable<InventoryItemListDto> Items { get; set; }
    }
}

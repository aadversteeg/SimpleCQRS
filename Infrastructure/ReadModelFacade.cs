using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class ReadModelFacade : IReadModelFacade
    {
        private IDatabase _database;

        public ReadModelFacade(IDatabase database)
        {
            _database = database;
        }

        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return _database.ListItems;
        }

        public InventoryItemDetailsDto GetInventoryItemDetails(Guid id)
        {
            return _database.DetailItems.First(d => d.Id == id);
        }
    }
}

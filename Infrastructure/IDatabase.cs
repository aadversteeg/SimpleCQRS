using System.Linq;

namespace Infrastructure
{
    public interface IDatabase
    {
        IQueryable<InventoryItemListDto> ListItems { get; }
        void Delete(InventoryItemListDto item);
        void Insert(InventoryItemListDto item);

        IQueryable<InventoryItemDetailsDto> DetailItems { get; }
        void Delete(InventoryItemDetailsDto item);
        void Insert(InventoryItemDetailsDto item);
    }
}

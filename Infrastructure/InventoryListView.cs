using Infrastructure.Events;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InventoryListView : 
        INotificationHandler<InventoryItemCreated>, 
        INotificationHandler<InventoryItemRenamed>, 
        INotificationHandler<InventoryItemDeactivated>
    {
        private readonly IDatabase _database;

        public InventoryListView(IDatabase database)
        {
            _database = database;
        }

        public Task Handle(InventoryItemCreated message, CancellationToken cancellationToken)
        {
            _database.Insert(new InventoryItemListDto(message.Id, message.Name));

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemRenamed message, CancellationToken cancellationToken)
        {
            var item = _database.ListItems.First(x => x.Id == message.Id);
            item.Name = message.NewName;

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemDeactivated message, CancellationToken cancellationToken)
        {
            var item = _database.ListItems.First(x => x.Id == message.Id);
            _database.Delete(item);

            return Task.CompletedTask;
        }
    }
}

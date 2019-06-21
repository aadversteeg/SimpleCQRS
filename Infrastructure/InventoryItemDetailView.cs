using Infrastructure.Events;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InventoryItemDetailView : 
        INotificationHandler<InventoryItemCreated>, 
        INotificationHandler<InventoryItemDeactivated>, 
        INotificationHandler<InventoryItemRenamed>, 
        INotificationHandler<ItemsRemovedFromInventory>, 
        INotificationHandler<ItemsCheckedInToInventory>
    {
        private readonly IDatabase _database;

        public InventoryItemDetailView(IDatabase database)
        {
            _database = database;
        }

        public Task Handle(InventoryItemCreated message, CancellationToken cancellationToken)
        {
            _database.Insert(new InventoryItemDetailsDto(message.Id, message.Name, 0, 0));

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemRenamed message, CancellationToken cancellationToken)
        {
            var d = _database.DetailItems.First(i => i.Id == message.Id);
            d.Name = message.NewName;
            d.Version = message.Version;

            return Task.CompletedTask;
        }

        public Task Handle(ItemsRemovedFromInventory message, CancellationToken cancellationToken)
        {
            var d = _database.DetailItems.First(i => i.Id == message.Id);
            d.CurrentCount -= message.Count;
            d.Version = message.Version;

            return Task.CompletedTask;
        }

        public Task Handle(ItemsCheckedInToInventory message, CancellationToken cancellationToken)
        {
            var d = _database.DetailItems.First(i => i.Id == message.Id);
            d.CurrentCount += message.Count;
            d.Version = message.Version;

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemDeactivated message, CancellationToken cancellationToken)
        {
            var d = _database.DetailItems.First(i => i.Id == message.Id);
            _database.Delete(d);

            return Task.CompletedTask;
        }
    }
}

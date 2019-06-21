using Infrastructure.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.InMemory
{
    public class InventoryListView : 
        INotificationHandler<InventoryItemCreated>, 
        INotificationHandler<InventoryItemRenamed>, 
        INotificationHandler<InventoryItemDeactivated>
    {
        public Task Handle(InventoryItemCreated message, CancellationToken cancellationToken)
        {
            BullShitDatabase.list.Add(new InventoryItemListDto(message.Id, message.Name));

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemRenamed message, CancellationToken cancellationToken)
        {
            var item = BullShitDatabase.list.Find(x => x.Id == message.Id);
            item.Name = message.NewName;

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemDeactivated message, CancellationToken cancellationToken)
        {
            BullShitDatabase.list.RemoveAll(x => x.Id == message.Id);

            return Task.CompletedTask;
        }
    }
}

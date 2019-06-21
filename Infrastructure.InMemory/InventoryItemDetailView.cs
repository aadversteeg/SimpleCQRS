using Infrastructure.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.InMemory
{
    public class InventoryItemDetailView : 
        INotificationHandler<InventoryItemCreated>, 
        INotificationHandler<InventoryItemDeactivated>, 
        INotificationHandler<InventoryItemRenamed>, 
        INotificationHandler<ItemsRemovedFromInventory>, 
        INotificationHandler<ItemsCheckedInToInventory>
    {
        public Task Handle(InventoryItemCreated message, CancellationToken cancellationToken)
        {
            BullShitDatabase.details.Add(message.Id, new InventoryItemDetailsDto(message.Id, message.Name, 0, 0));

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemRenamed message, CancellationToken cancellationToken)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.Name = message.NewName;
            d.Version = message.Version;

            return Task.CompletedTask;
        }

        public Task Handle(ItemsRemovedFromInventory message, CancellationToken cancellationToken)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.CurrentCount -= message.Count;
            d.Version = message.Version;

            return Task.CompletedTask;
        }

        public Task Handle(ItemsCheckedInToInventory message, CancellationToken cancellationToken)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.CurrentCount += message.Count;
            d.Version = message.Version;

            return Task.CompletedTask;
        }

        public Task Handle(InventoryItemDeactivated message, CancellationToken cancellationToken)
        {
            BullShitDatabase.details.Remove(message.Id);

            return Task.CompletedTask;
        }

        private InventoryItemDetailsDto GetDetailsItem(Guid id)
        {
            InventoryItemDetailsDto d;

            if (!BullShitDatabase.details.TryGetValue(id, out d))
            {
                throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
            }

            return d;
        }
    }
}

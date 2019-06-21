using Application;
using Domain;
using System;
using System.Linq;

namespace Infrastructure
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public Event Map(Domain.Events.DomainEvent @event)
        {
            if(@event is Domain.Events.InventoryItemCreated inventoryItemCreated)
            {
                return new Infrastructure.Events.InventoryItemCreated(inventoryItemCreated.Id, inventoryItemCreated.Name);
            }
            else if (@event is Domain.Events.InventoryItemDeactivated inventoryItemDeactivated)
            {
                return new Infrastructure.Events.InventoryItemDeactivated(inventoryItemDeactivated.Id);
            }
            else if (@event is Domain.Events.InventoryItemRenamed inventoryItemRenamed)
            {
                return new Infrastructure.Events.InventoryItemRenamed(inventoryItemRenamed.Id, inventoryItemRenamed.NewName);
            }
            else if (@event is Domain.Events.ItemsCheckedInToInventory itemsCheckedInToInventory)
            {
                return new Infrastructure.Events.ItemsCheckedInToInventory(itemsCheckedInToInventory.Id, itemsCheckedInToInventory.Count);
            }
            else if (@event is Domain.Events.ItemsRemovedFromInventory itemsRemovedFromInventory)
            {
                return new Infrastructure.Events.ItemsRemovedFromInventory(itemsRemovedFromInventory.Id, itemsRemovedFromInventory.Count);
            }
            throw new Exception();
        }

        public Domain.Events.DomainEvent Map(Infrastructure.Event @event)
        {
            if (@event is Infrastructure.Events.InventoryItemCreated inventoryItemCreated)
            {
                return new Domain.Events.InventoryItemCreated(inventoryItemCreated.Id, inventoryItemCreated.Name);
            }
            else if (@event is Infrastructure.Events.InventoryItemDeactivated inventoryItemDeactivated)
            {
                return new Domain.Events.InventoryItemDeactivated(inventoryItemDeactivated.Id);
            }
            else if (@event is Infrastructure.Events.InventoryItemRenamed inventoryItemRenamed)
            {
                return new Domain.Events.InventoryItemRenamed(inventoryItemRenamed.Id, inventoryItemRenamed.NewName);
            }
            else if (@event is Infrastructure.Events.ItemsCheckedInToInventory itemsCheckedInToInventory)
            {
                return new Domain.Events.ItemsCheckedInToInventory(itemsCheckedInToInventory.Id, itemsCheckedInToInventory.Count);
            }
            else if (@event is Infrastructure.Events.ItemsRemovedFromInventory itemsRemovedFromInventory)
            {
                return new Domain.Events.ItemsRemovedFromInventory(itemsRemovedFromInventory.Id, itemsRemovedFromInventory.Count);
            }
            throw new Exception();
        }


        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges().Select(e => Map(e)), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var events = _storage.GetEventsForAggregate(id).Select( e => Map(e));
            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}

using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    class RemoveItemsFromInventoryHandler : IRequestHandler<RemoveItemsFromInventory>
    {
        private readonly IRepository<InventoryItem> _repository;

        public RemoveItemsFromInventoryHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(RemoveItemsFromInventory request, CancellationToken cancellationToken)
        {
            var item = _repository.GetById(request.InventoryItemId);
            item.Remove(request.Count);
            _repository.Save(item, request.OriginalVersion);

            return Unit.Task;
        }
    }
}


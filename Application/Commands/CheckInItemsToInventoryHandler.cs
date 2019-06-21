using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    class CheckInItemsToInventoryHandler : IRequestHandler<CheckInItemsToInventory>
    {
        private readonly IRepository<InventoryItem> _repository;

        public CheckInItemsToInventoryHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(CheckInItemsToInventory request, CancellationToken cancellationToken)
        {
            var item = _repository.GetById(request.InventoryItemId);
            item.CheckIn(request.Count);
            _repository.Save(item, request.OriginalVersion);

            return Unit.Task;
        }
    }
}

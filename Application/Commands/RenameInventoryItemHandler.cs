using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    class RenameInventoryItemHandler : IRequestHandler<RenameInventoryItem>
    {
        private readonly IRepository<InventoryItem> _repository;

        public RenameInventoryItemHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(RenameInventoryItem request, CancellationToken cancellationToken)
        {
            var item = _repository.GetById(request.InventoryItemId);
            item.ChangeName(request.NewName);
            _repository.Save(item, request.OriginalVersion);

            return Unit.Task;
        }
    }
}

using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeactivateInventoryItemHandler : IRequestHandler<DeactivateInventoryItem>
    {
        private readonly IRepository<InventoryItem> _repository;

        public DeactivateInventoryItemHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(DeactivateInventoryItem request, CancellationToken cancellationToken)
        {
            var item = _repository.GetById(request.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, request.OriginalVersion);

            return Unit.Task;
        }
    }
}

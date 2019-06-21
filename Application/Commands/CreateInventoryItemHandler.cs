using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;

namespace Application.Commands
{
    class CreateInventoryItemHandler : IRequestHandler<CreateInventoryItem>
    {
        private readonly IRepository<InventoryItem> _repository;

        public CreateInventoryItemHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(CreateInventoryItem request, CancellationToken cancellationToken)
        {
            var item = new InventoryItem(request.InventoryItemId, request.Name);
            _repository.Save(item, -1);

            return Unit.Task;
        }
    }
}

using JewerlyGala.Domain.Repositories;
using MediatR;

namespace JewerlyGala.Application.Features.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommand : IRequest<Guid>
    {
        public string SupplierName { get; set; } = default!;
    }

    public class CreateSupplierCommandHandler(
            ISupplierRepository supplierRepository
        ) : IRequestHandler<CreateSupplierCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {

            var supplierId = await supplierRepository.CreateAsync(request.SupplierName);

            return supplierId;
        }
    }
}

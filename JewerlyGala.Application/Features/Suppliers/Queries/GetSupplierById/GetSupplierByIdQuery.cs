using AutoMapper;
using JewerlyGala.Application.Features.Suppliers.DTOs;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdQuery : IRequest<SupplierDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetAllSuppliersQueryHandler(
        ILogger<GetAllSuppliersQueryHandler> logger,
        IMapper mapper,
        ISupplierRepository supplierRepository
        ) : IRequestHandler<GetSupplierByIdQuery, SupplierDTO>
    {
        public async Task<SupplierDTO> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("getting supplier by id");

            var suppliers = await supplierRepository.GetById(request.Id);

            if (suppliers == null)
            {
                throw new NotFoundException("supplier not found");
            }

            var Supplier = mapper.Map<SupplierDTO>(suppliers);

            return Supplier;
        }
    }
}

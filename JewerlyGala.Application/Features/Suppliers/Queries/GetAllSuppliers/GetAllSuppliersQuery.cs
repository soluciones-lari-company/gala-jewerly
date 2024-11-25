using AutoMapper;
using JewerlyGala.Application.Features.Suppliers.DTOs;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Suppliers.Queries.GetAllSuppliers
{
    public class GetAllSuppliersQuery : IRequest<IEnumerable<SupplierDTO>>
    {
    }

    public class GetAllSuppliersQueryHandler(
        ILogger<GetAllSuppliersQueryHandler> logger,
        IMapper mapper,
        ISupplierRepository supplierRepository
        ) : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierDTO>>
    {
        public async Task<IEnumerable<SupplierDTO>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("getting all suppliers");


            var suppliers = await supplierRepository.GetAllAsync();

            var modelsDto = mapper.Map<IEnumerable<SupplierDTO>>(suppliers);

            return modelsDto;
        }
    }
}

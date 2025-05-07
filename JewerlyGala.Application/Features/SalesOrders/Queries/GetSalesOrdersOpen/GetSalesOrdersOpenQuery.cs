using AutoMapper;
using JewerlyGala.Application.Features.SalesOrders.DTOs;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Queries.GetSalesOrdersOpen
{
    public class GetSalesOrdersOpenQuery: IRequest<ICollection<SalesOrderDTO>>
    {
    }

    public class GetSalesOrdersOpenQueryHandler(
                ILogger<GetSalesOrdersOpenQuery> logger,
        IMapper mapper,
        ISalesOrderRepository salesOrderRepository
        ) : IRequestHandler<GetSalesOrdersOpenQuery, ICollection<SalesOrderDTO>>
    {
        public async Task<ICollection<SalesOrderDTO>> Handle(GetSalesOrdersOpenQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running GetSalesOrdersOpenQuery");

            var orders = await salesOrderRepository.GetOpenAsync();

            return mapper.Map<ICollection<SalesOrderDTO>>(orders);
        }
    }
}

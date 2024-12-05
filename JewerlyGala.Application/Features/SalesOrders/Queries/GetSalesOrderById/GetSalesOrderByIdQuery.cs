using AutoMapper;
using JewerlyGala.Application.Features.SalesOrders.DTOs;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Queries.GetSalesOrderById
{
    public class GetSalesOrderByIdQuery : IRequest<SalesOrderDTO>
    {
        public Guid IdSalesOrder { get; set; }
    }

    public class GetSalesOrderByIdQueryHandler(
        ILogger<GetSalesOrderByIdQueryHandler> logger,
        IMapper mapper,
        ISalesOrderRepository salesOrderRepository
        ) : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderDTO>
    {
        public async Task<SalesOrderDTO> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running GetSalesOrderByIdQueryHandler");

            var order = await salesOrderRepository.GetByIdAsync(request.IdSalesOrder);

            if(!order)
            {
                throw new NotFoundException("sales order not found");
            }


            return mapper.Map<SalesOrderDTO>(salesOrderRepository.Order);
        }
    }
}

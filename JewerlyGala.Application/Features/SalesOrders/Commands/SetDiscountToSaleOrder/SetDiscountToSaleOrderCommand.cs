using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.SetDiscountToSaleOrder
{
    public class SetDiscountToSaleOrderCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
        public decimal DiscountPercentaje { get; set; }
    }

    public class SetDiscountToSaleOrderCommandHandler(
        ILogger<SetDiscountToSaleOrderCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository
        ) : IRequestHandler<SetDiscountToSaleOrderCommand>
    {
        public async Task Handle(SetDiscountToSaleOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running AddLineToSalesOrderCommand");
            
            if (request.DiscountPercentaje < 0)
            {
                throw new InvalidParamException("DiscountPercentaje not valid");
            }

            var order = await salesOrderRepository.GetByIdAsync(request.SalesOrderId);

            if (!order)
            {
                throw new NotFoundException("sales order not found");
            }

            if (salesOrderRepository.Order.CanceledAt != null)
            {
                throw new NotFoundException("sales order canceled");
            }

            if (salesOrderRepository.Order.ConfirmedAt != null)
            {
                throw new NotFoundException("sales order has been confirmed");
            }

            salesOrderRepository.Order.DiscountPercentaje = request.DiscountPercentaje;

            //orden en progreso
            if (salesOrderRepository.Order.SaleOrderLinesNavigation.Count > 0)
            {
                foreach (var line in salesOrderRepository.Order.SaleOrderLinesNavigation)
                {
                    line.DiscountPercentaje = salesOrderRepository.Order.DiscountPercentaje;
                    //line.SubTotal = line.Quantity * line.UnitPrice;
                    line.DiscountTotal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.SubTotal * (salesOrderRepository.Order.DiscountPercentaje / 100)) : 0;
                    line.Total = line.SubTotal - line.DiscountTotal;
                    line.UnitPriceFinal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.Total / line.Quantity) : line.UnitPrice;
                }
            }

            salesOrderRepository.Order.SubTotal = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.SubTotal);
            salesOrderRepository.Order.DiscountTotal = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.DiscountTotal);
            salesOrderRepository.Order.Total = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.Total);

            await salesOrderRepository.UpdateAsync();

        }
    }
}

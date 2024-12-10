using Azure.Core;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.DeleteLineFromOrder
{
    public class DeleteLineFromOrderCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
        public Guid ItemSerieId { get; set; }
        public int Quantity { get; set; }
    }

    public class DeleteLineFromOrderCommandHandler(
        ILogger<DeleteLineFromOrderCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        IItemSerieRepository itemSerieRepository
        ) : IRequestHandler<DeleteLineFromOrderCommand>
    {
        public async Task Handle(DeleteLineFromOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running DeleteLineFromOrderCommandHandler");

            var order = await salesOrderRepository.GetByIdAsync(request.SalesOrderId);

            if (!order)
            {
                throw new NotFoundException("sales order not found");
            }

            if (salesOrderRepository.Order.CanceledAt != null)
            {
                throw new InvalidOperationException("sales order canceled");
            }

            if (salesOrderRepository.Order.ConfirmedAt != null)
            {
                throw new InvalidOperationException("sales order has been confirmed");
            }

            var line = salesOrderRepository.Order.SaleOrderLinesNavigation.FirstOrDefault(e => e.ItemSerieId == request.ItemSerieId);

            if(line == null)
            {
                throw new NotFoundException("the serie is not found into the order selected");
            }

            if(request.Quantity == line.Quantity)
            {
                salesOrderRepository.Order.SaleOrderLinesNavigation.Remove(line);
            }
            else
            {
                if(request.Quantity < line.Quantity)
                {
                    line.Quantity -= request.Quantity;
                    line.DiscountTotal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.SubTotal * (salesOrderRepository.Order.DiscountPercentaje / 100)) : 0;
                    line.Total = line.SubTotal - line.DiscountTotal;
                    line.UnitPriceFinal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.Total / line.Quantity) : line.UnitPrice;
                }
            }

            await UpdateOrder();

            await UpdateSerie(request.ItemSerieId, request.Quantity);
        }

        private async Task UpdateOrder()
        {
            salesOrderRepository.Order.SubTotal = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.SubTotal);
            salesOrderRepository.Order.DiscountTotal = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.DiscountTotal);
            salesOrderRepository.Order.Total = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.Total);

            await salesOrderRepository.UpdateAsync();
        }

        private async Task UpdateSerie(Guid itemSerieId, int quantity)
        {
            var serie = await itemSerieRepository.GetByIdAsync(itemSerieId);

            if (serie == null)
            {
                throw new NotFoundException("serie not found");
            }

            serie.QuantityCommited -= quantity;
            serie.QuantityFree = serie.Quantity - serie.QuantityCommited - serie.QuantitySold;

            await itemSerieRepository.UpdateAsync(serie.Id, serie);
        }
    }
}

using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.CancelSalesOrder
{
    public class CancelSalesOrderCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
    }

    public class CancelSalesOrderCommandHandler(
        ILogger<CancelSalesOrderCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        IItemSerieRepository itemSerieRepository
        ) : IRequestHandler<CancelSalesOrderCommand>
    {
        public async Task Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running CancelSalesOrderCommand");

            var order = await salesOrderRepository.GetByIdAsync(request.SalesOrderId);

            if (!order)
            {
                throw new NotFoundException("sales order not found");
            }

            if (salesOrderRepository.Order.CanceledAt != null)
            {
                throw new InvalidOperationException("sales order canceled");
            }

            if (salesOrderRepository.Order.CanceledAt == null)
            {
                if (salesOrderRepository.Order.ConfirmedAt == null)
                {
                    //orden en progreso
                    if (salesOrderRepository.Order.SaleOrderLinesNavigation.Count > 0)
                    {
                        foreach (var line in salesOrderRepository.Order.SaleOrderLinesNavigation)
                        {
                            var serie = await itemSerieRepository.GetByIdAsync(line.ItemSerieId);
                            if (serie != null)
                            {
                                //serie.QuantitySold += line.Quantity;
                                serie.QuantityCommited -= line.Quantity;
                                serie.QuantityFree = serie.Quantity - serie.QuantityCommited - serie.QuantitySold;

                                await itemSerieRepository.UpdateAsync(serie.Id, serie);
                            }
                        }
                    }
                }
                else
                {
                    // orden confirmada
                    if (salesOrderRepository.Order.SaleOrderLinesNavigation.Count > 0)
                    {
                        foreach (var line in salesOrderRepository.Order.SaleOrderLinesNavigation)
                        {
                            var serie = await itemSerieRepository.GetByIdAsync(line.ItemSerieId);
                            if (serie != null)
                            {
                                serie.QuantitySold -= line.Quantity;
                                //serie.QuantityCommited -= line.Quantity;
                                serie.QuantityFree = serie.Quantity - serie.QuantityCommited - serie.QuantitySold;

                                await itemSerieRepository.UpdateAsync(serie.Id, serie);
                            }
                        }
                    }
                }

                salesOrderRepository.Order.CanceledAt = DateTime.Now;

                await salesOrderRepository.UpdateAsync();
            }
        }
    }
}

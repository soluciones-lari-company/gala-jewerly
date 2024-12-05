using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;
using JewerlyGala.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using JewerlyGala.Domain.Repositories;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.ConfirmSaleOrder
{
    public class ConfirmSaleOrderCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
    }

    public class ConfirmSaleOrderCommandHandler(
        ILogger<ConfirmSaleOrderCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        IItemSerieRepository itemSerieRepository
        ) : IRequestHandler<ConfirmSaleOrderCommand>
    {
        public async Task Handle(ConfirmSaleOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running ConfirmSaleOrderCommand");

            var order = await salesOrderRepository.GetByIdAsync(request.SalesOrderId);

            if (!order)
            {
                throw new NotFoundException("sales order not found");
            }

            if (salesOrderRepository.Order.PaymentTerms.IsNullOrEmpty() || salesOrderRepository.Order.PaymentMethod.IsNullOrEmpty())
            {
                throw new InvalidParamException("Please add payment information to this order first");
            }

            if(salesOrderRepository.Order.CanceledAt != null)
            {
                throw new NotFoundException("sales order canceled");
            }

            if (salesOrderRepository.Order.ConfirmedAt != null)
            {
                throw new NotFoundException("sales order has been confirmed");
            }

            if (salesOrderRepository.Order.Total <= 0 || salesOrderRepository.Order.SaleOrderLinesNavigation.Count() == 0)
            {
                throw new InvalidParamException("Please add items to this order first");
            }

            salesOrderRepository.Order.ConfirmedAt = DateTime.Now;

            foreach(var line in salesOrderRepository.Order.SaleOrderLinesNavigation)
            {
                var serie = await itemSerieRepository.GetByIdAsync(line.ItemSerieId);
                if(serie != null)
                {
                    serie.QuantitySold += line.Quantity;
                    serie.QuantityCommited -= line.Quantity;
                    serie.QuantityFree = serie.Quantity - serie.QuantityCommited - serie.QuantitySold;

                    await itemSerieRepository.UpdateAsync(serie.Id, serie);
                }
            }


            await salesOrderRepository.UpdateAsync();
        }
    }
}

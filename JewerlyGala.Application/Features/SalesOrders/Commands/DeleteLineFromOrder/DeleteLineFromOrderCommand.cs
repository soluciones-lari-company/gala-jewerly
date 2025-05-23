using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        IJewerlyDbContext dbContext
        ) : IRequestHandler<DeleteLineFromOrderCommand>
    {
        public async Task Handle(DeleteLineFromOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running DeleteLineFromOrderCommandHandler");

            var order = await dbContext.SalesOrders
                .Include(e => e.SaleOrderLinesNavigation)
                .FirstOrDefaultAsync(x => x.Id == request.SalesOrderId);

            SalesOrderOperations.IsOrderEditable(order);

            var line = order.SaleOrderLinesNavigation.FirstOrDefault(e => e.ItemSerieId == request.ItemSerieId) ?? 
                throw new NotFoundException("Serie no encontrada en la orden seleccionada");


            if (request.Quantity == line.Quantity)
            {
                order.SaleOrderLinesNavigation.Remove(line);
            }
            else
            {
                if (request.Quantity < line.Quantity)
                {
                    line.Quantity -= request.Quantity;
                    line.SubTotal = line.Quantity * line.UnitPrice;
                    line.DiscountTotal = line.DiscountPercentaje > 0 ? (line.SubTotal * (line.DiscountPercentaje / 100)) : 0;
                    line.Total = line.SubTotal - line.DiscountTotal;
                    line.UnitPriceFinal = line.Total / line.Quantity;
                }
            }

            SalesOrderOperations.CalculateCosts(order);

            var serie = await dbContext.ItemSeries.FirstOrDefaultAsync(e =>  e.Id == line.ItemSerieId) ??
                throw new NotFoundException("la serie no fue encontrada");

            serie.QuantityCommited -= request.Quantity;
            serie.QuantityFree = serie.Quantity - serie.QuantityCommited - serie.QuantitySold; 


            await dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}

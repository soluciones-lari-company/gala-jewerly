using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Enums;
using JewerlyGala.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder
{
    public class AddLineToSalesOrderCommand : IRequest<int>
    {
        public Guid SalesOrderId { get; set; }
        public string SerieCode { get; set; } = default!;
        public int Quantity { get; set; }
        //public decimal UnitPrice { get; set; }
    }

    public class AddLineToSalesOrderCommandHandler(
        ILogger<AddLineToSalesOrderCommandHandler> logger,
        IJewerlyDbContext dbContext
        ) : IRequestHandler<AddLineToSalesOrderCommand, int>
    {
        public async Task<int> Handle(AddLineToSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running AddLineToSalesOrderCommand");

            var order = await dbContext.SalesOrders
                .Include(e => e.SaleOrderLinesNavigation)
                .FirstOrDefaultAsync(x => x.Id == request.SalesOrderId);

            SalesOrderOperations.IsOrderEditable(order);

            var serieSelected = await dbContext.ItemSeries.FirstOrDefaultAsync(x => x.SerieCode == request.SerieCode);
            if (serieSelected == null)
            {
                throw new NotFoundException("Serie no encontrada");
            }
            else
            {
                if (serieSelected.QuantityFree < request.Quantity)
                {
                    throw new InvalidOperationException($"La serie {serieSelected.SerieCode} dispone unicamente de {serieSelected.QuantityFree} pieza(s) para ordernar");
                }
            }

            var line = order.SaleOrderLinesNavigation.FirstOrDefault(e => e.ItemSerieId == serieSelected.Id);
            if (line == null)
            {
                // define new line
                line = new SaleOrderLine();
                line.SalesOrderId = order.Id;
                line.NumLine = order.SaleOrderLinesNavigation.Count() + 1;
                line.ItemSerieId = serieSelected.Id;
                line.SerieCode = serieSelected.SerieCode;
                line.Descripcion = serieSelected.Description;
                line.TypeLine = serieSelected.Type;
                line.Quantity = request.Quantity;
                line.UnitPrice = serieSelected.SaleUnitPrice;
                line.SubTotal = line.Quantity * line.UnitPrice;
                line.DiscountPercentaje = 0;
                line.DiscountTotal = 0;
                line.Total = line.SubTotal - line.DiscountTotal;
                line.UnitPriceFinal = order.DiscountPercentaje > 0 ? (line.UnitPrice - line.UnitPrice * (line.DiscountPercentaje / 100)) : line.UnitPrice;
                //4433 3 2 dias 4422721616 4428180729 rodrigo 6800 
                // add new line to order
                order.SaleOrderLinesNavigation.Add(line);
            }
            else
            {
                line.Quantity += request.Quantity;
                line.SubTotal = line.Quantity * line.UnitPrice;
                line.DiscountPercentaje = 0;
                line.DiscountTotal = 0;
                line.Total = line.SubTotal - line.DiscountTotal;
                line.UnitPriceFinal = order.DiscountPercentaje > 0 ? (line.UnitPrice - line.UnitPrice * (line.DiscountPercentaje / 100)) : line.UnitPrice;
            }

            //update quantity in serie
            serieSelected.QuantityCommited += request.Quantity;
            serieSelected.QuantityFree = serieSelected.Quantity - serieSelected.QuantityCommited - serieSelected.QuantitySold;

            SalesOrderOperations.CalculateCosts(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return line.Id;
        }
    }
}

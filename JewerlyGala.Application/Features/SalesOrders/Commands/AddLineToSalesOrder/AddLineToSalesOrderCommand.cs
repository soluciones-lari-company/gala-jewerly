using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder
{
    public class AddLineToSalesOrderCommand : IRequest<int>
    {
        public Guid SalesOrderId { get; set; }
        public int NumLine { get; set; }
        public Guid ItemSerieId { get; set; }
        //public string SerieCode { get; set; } = default!;
        //public string Descripcion { get; set; } = default!;
        public int Quantity { get; set; }
        //public decimal UnitPrice { get; set; }
        //public decimal SubTotal { get; set; }
        //public decimal DiscountPercentaje { get; set; }
        //public decimal DiscountTotal { get; set; }
        //public decimal Total { get; set; }
        //public decimal UnitPriceFinal { get; set; }
    }

    public class AddLineToSalesOrderCommandHandler(
        ILogger<AddLineToSalesOrderCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        IItemSerieRepository itemSerieRepository
        ) : IRequestHandler<AddLineToSalesOrderCommand, int>
    {
        public async Task<int> Handle(AddLineToSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running AddLineToSalesOrderCommand");

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

            var serie = await itemSerieRepository.GetByIdAsync(request.ItemSerieId);

            if(serie == null)
            {
                throw new NotFoundException("serie not found");
            }

            if (serie != null && serie.QuantityFree < request.Quantity)
            {
                throw new InvalidParamException($"the serie {serie.SerieCode} has only {serie.QuantityFree} items available to order");
            }

            // agregar linea
            var newLine = await AddLine(serie, request);
            await UpdateSerie(serie, request.Quantity);

            await UpdateOrder();

            return newLine.Id;
        }

        private async Task UpdateSerie(ItemSerie serie, int quantity)
        {
            serie.QuantityCommited += quantity;
            serie.QuantityFree = serie.Quantity - serie.QuantityCommited - serie.QuantitySold;

            await itemSerieRepository.UpdateAsync(serie.Id, serie);
        }

        private async Task<SaleOrderLine> AddLine(ItemSerie serie, AddLineToSalesOrderCommand request)
        {
            var line = salesOrderRepository.Order.SaleOrderLinesNavigation.FirstOrDefault(e => e.ItemSerieId == serie.Id);
            if(line == null)
            {
                line = new SaleOrderLine();
                line.SalesOrderId = salesOrderRepository.Order.Id;
                line.NumLine = request.NumLine;
                line.ItemSerieId = serie.Id;
                line.SerieCode = serie.SerieCode;
                line.Descripcion = serie.Description;
                line.Quantity = request.Quantity;
                line.UnitPrice = serie.SaleUnitPrice;
                line.SubTotal = line.Quantity * serie.SaleUnitPrice;
                line.DiscountPercentaje = salesOrderRepository.Order.DiscountPercentaje;
                line.DiscountTotal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.SubTotal * (salesOrderRepository.Order.DiscountPercentaje / 100)) : 0;
                line.Total = line.SubTotal - line.DiscountTotal;
                line.UnitPriceFinal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.Total / line.Quantity) : line.UnitPrice;


                salesOrderRepository.Order.SaleOrderLinesNavigation.Add(line);
            }
            else
            {
                line.Quantity += request.Quantity;
                //line.UnitPrice = serie.SaleUnitPrice;
                line.SubTotal = line.Quantity * serie.SaleUnitPrice;
                line.DiscountPercentaje = salesOrderRepository.Order.DiscountPercentaje;
                line.DiscountTotal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.SubTotal * (salesOrderRepository.Order.DiscountPercentaje / 100)) : 0;
                line.Total = line.SubTotal - line.DiscountTotal;
                line.UnitPriceFinal = salesOrderRepository.Order.DiscountPercentaje > 0 ? (line.Total / line.Quantity) : line.UnitPrice;
            }

            await salesOrderRepository.UpdateAsync();

            //line.Id = await salesOrderRepository.AddLineAsync(request.SalesOrderId, line);

            return line;
        }

        private async Task UpdateOrder()
        {
            salesOrderRepository.Order.SubTotal = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.SubTotal);
            salesOrderRepository.Order.DiscountTotal = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.DiscountTotal);
            salesOrderRepository.Order.Total = salesOrderRepository.Order.SaleOrderLinesNavigation.Sum(e => e.Total);

            await salesOrderRepository.UpdateAsync();
        }
    }
}

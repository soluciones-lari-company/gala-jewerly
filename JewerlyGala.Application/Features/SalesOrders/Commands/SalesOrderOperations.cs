using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Enums;
using JewerlyGala.Domain.Exceptions;

namespace JewerlyGala.Application.Features.SalesOrders.Commands
{
    public static class SalesOrderOperations
    {
        public static void IsOrderEditable(SalesOrder order)
        {
            if (order == null)
                throw new NotFoundException("sales order not found");

            if (order.CanceledAt != null)
                throw new InvalidOperationException("sales order canceled");

            if (order.ConfirmedAt != null)
                throw new InvalidOperationException("sales order has been confirmed");
        }
        public static void CalculateCosts(SalesOrder order)
        {
            if (order == null)
            {
                throw new InvalidOperationException("the order selected was not found");
            }

            // sum all the series typed as Item
            order.SubTotal = order.SaleOrderLinesNavigation.Where(e => e.TypeLine == TypeSerie.Item).Sum(e => e.SubTotal);
            // sum all the series typed as workshop
            order.WorkshopCost = order.SaleOrderLinesNavigation.Where(e => e.TypeLine == TypeSerie.WorkShopService).Sum(e => e.SubTotal);
            order.DiscountTotal = order.DiscountPercentaje > 0 ? (order.SubTotal * (order.DiscountPercentaje / 100)) : 0;
            order.Total = order.SubTotal - order.DiscountTotal + order.WorkshopCost ?? 0;

            foreach (var line in order.SaleOrderLinesNavigation)
            {
                //allow discount only for serias setting up as Item
                if (line.TypeLine == TypeSerie.Item)
                {
                    line.DiscountPercentaje = order.DiscountPercentaje;
                    line.UnitPriceFinal = order.DiscountPercentaje > 0 ? (line.UnitPrice - line.UnitPrice * (line.DiscountPercentaje / 100)) : line.UnitPrice;
                }
                else
                {
                    line.DiscountPercentaje = 0;
                    line.UnitPriceFinal = line.UnitPrice;
                }

                line.SubTotal = line.Quantity * line.UnitPrice;
                line.DiscountPercentaje = 0;
                line.DiscountTotal = 0;
                line.Total = line.SubTotal - line.DiscountTotal;
                
            }
        }
    }
}

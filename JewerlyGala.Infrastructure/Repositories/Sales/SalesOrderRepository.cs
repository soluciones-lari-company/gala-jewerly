using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories.Sales
{
    public class SalesOrderRepository(JewerlyDbContext dbContext) : ISalesOrderRepository
    {
        public SalesOrder Order { get; set; } = new SalesOrder();

        public async Task<Guid> CreateAsync()
        {
            if(Order.Id != Guid.Empty)
            {
                throw new InvalidOperationException(nameof(Order.Id));
            }

            await dbContext.SalesOrders.AddAsync(Order);

            await dbContext.SaveChangesAsync();

            return Order.Id;
        }

        public async Task<ICollection<SalesOrder>> GetAllAsync()
        {
            var orders = await dbContext.SalesOrders.Include(e => e.CustomerNavigation).ToListAsync();
            return orders;
        }

        public async Task<bool> UpdateAsync()
        {
            if (Order.Id == Guid.Empty)
            {
                throw new InvalidOperationException(nameof(Order.Id));
            }

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> GetByIdAsync(Guid idsalesOrder)
        {
            var order_ = await dbContext.SalesOrders
                .Include(e => e.CustomerNavigation)
                .Include(e => e.SaleOrderLinesNavigation)
                .FirstOrDefaultAsync(e => e.Id == idsalesOrder);


            if(order_ == null)
            {
                return false;
            }
            else
            {
                Order = order_;
                return true;
            }
        }
        #region oldcode
        //public async Task<int> AddLineAsync(Guid idsalesOrder, SaleOrderLine line)
        //{
        //    await dbContext.SalesOrderLines.AddAsync(line);

        //    await dbContext.SaveChangesAsync();

        //    return line.Id;
        //}

        //public async Task<Guid> CreateAsync(SalesOrder order)
        //{
        //    await dbContext.SalesOrders.AddAsync(order);

        //    await dbContext.SaveChangesAsync();

        //    return order.Id;
        //}

        //public async Task<ICollection<SalesOrder>> GetAllAsync()
        //{
        //    var orders = await dbContext.SalesOrders.Include(e => e.CustomerNavigation).ToListAsync();
        //    return orders;
        //}

        //public async Task<SalesOrder?> GetByIdAsync(Guid idsalesOrder)
        //{
        //    var order_ = await dbContext.SalesOrders
        //        .Include(e => e.CustomerNavigation)
        //        .Include(e => e.SaleOrderLinesNavigation)
        //        .FirstOrDefaultAsync(e => e.Id == idsalesOrder);

        //    return order_;
        //}

        //public async Task RemoveLineAsync(Guid idsalesOrder, SaleOrderLine line)
        //{
        //    dbContext.SalesOrderLines.Remove(line);

        //    await dbContext.SaveChangesAsync();
        //}

        //public async Task UpdateAsync(SalesOrder order)
        //{
        //    var salesOrder = await dbContext.SalesOrders.FirstOrDefaultAsync(e => e.Id == order.Id);

        //    if (salesOrder != null)
        //    {
        //        salesOrder.IdCustomer = order.IdCustomer;
        //        salesOrder.Date = order.Date;
        //        salesOrder.DueDate = order.DueDate;
        //        salesOrder.PaymentTerms = order.PaymentTerms;
        //        salesOrder.PaymentMethod = order.PaymentMethod;
        //        salesOrder.PaymentConditions = order.PaymentConditions;
        //        salesOrder.SubTotal = order.SubTotal;
        //        salesOrder.DiscountPercentaje = order.DiscountPercentaje;
        //        salesOrder.DiscountTotal = order.DiscountTotal;
        //        salesOrder.Total = order.Total;
        //        salesOrder.Zone = order.Zone;
        //        salesOrder.ConfirmedAt = order.ConfirmedAt;
        //        salesOrder.CanceledAt = order.CanceledAt;

        //        await dbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task UpdateLineAsync(Guid idsalesOrder, SaleOrderLine line)
        //{
        //    var salesOrderLine = await dbContext.SalesOrderLines.FirstOrDefaultAsync(e => e.Id == line.Id);

        //    if (salesOrderLine != null)
        //    {
        //        salesOrderLine.Id = line.Id;
        //        salesOrderLine.SalesOrderId = idsalesOrder;
        //        salesOrderLine.NumLine = line.NumLine;
        //        salesOrderLine.ItemSerieId = line.ItemSerieId;
        //        salesOrderLine.SerieCode = line.SerieCode;
        //        salesOrderLine.Descripcion = line.Descripcion;
        //        salesOrderLine.Quantity = line.Quantity;
        //        salesOrderLine.UnitPrice = line.UnitPrice;
        //        salesOrderLine.SubTotal = line.SubTotal;
        //        salesOrderLine.DiscountPercentaje = line.DiscountPercentaje;
        //        salesOrderLine.DiscountTotal = line.DiscountTotal;
        //        salesOrderLine.Total = line.Total;
        //        salesOrderLine.UnitPriceFinal = line.UnitPriceFinal;

        //        await dbContext.SaveChangesAsync();
        //    }
        //}
        #endregion
    }
}

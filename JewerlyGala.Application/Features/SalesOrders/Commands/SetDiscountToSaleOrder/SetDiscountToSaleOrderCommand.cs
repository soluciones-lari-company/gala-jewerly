using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        IJewerlyDbContext dbContext
        ) : IRequestHandler<SetDiscountToSaleOrderCommand>
    {
        public async Task Handle(SetDiscountToSaleOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running AddLineToSalesOrderCommand");
            
            if (request.DiscountPercentaje < 0)
            {
                throw new InvalidParamException("DiscountPercentaje not valid");
            }

            var order = await dbContext.SalesOrders
                .Include(e => e.SaleOrderLinesNavigation)
                .FirstOrDefaultAsync(x => x.Id == request.SalesOrderId);

            SalesOrderOperations.IsOrderEditable(order);

            order.DiscountPercentaje = request.DiscountPercentaje;

            SalesOrderOperations.CalculateCosts(order);

            await dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}

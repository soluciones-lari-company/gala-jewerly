using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.SetWorkshopCostToOrder
{
    public class SetWorkshopCostToOrderCommand: IRequest
    {
        public Guid SalesOrderId { get; set; }
        public decimal WorkshopCost { get; set; }
    }

    public class SetWorkshopCostToOrderCommandHandler(
        ILogger<SetWorkshopCostToOrderCommandHandler> logger,
        IJewerlyDbContext dbContext
        ) : IRequestHandler<SetWorkshopCostToOrderCommand>
    {
        public async Task Handle(SetWorkshopCostToOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running SetWorkshopCostToOrderCommand");

            if (request.WorkshopCost < 0)
            {
                throw new InvalidParamException("WorkshopCost not valid");
            }

            var order = await dbContext.SalesOrders
                .Include(e => e.SaleOrderLinesNavigation)
                .FirstOrDefaultAsync( x => x.Id == request.SalesOrderId);

            SalesOrderOperations.IsOrderEditable(order);

            order.WorkshopCost = request.WorkshopCost;

            SalesOrderOperations.CalculateCosts(order);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

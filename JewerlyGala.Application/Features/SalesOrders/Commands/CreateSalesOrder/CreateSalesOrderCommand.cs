using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.CreateSalesOrder
{
    public class CreateSalesOrderCommand : IRequest<Guid>
    {
        public Guid IdCustomer { get; set; }
        public DateOnly Date { get; set; }
        public string Zone { get; set; } = default!;
    }

    public class CreateSalesOrderCommandHandler(
        ILogger<CreateSalesOrderCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        ICustomerRepository customerRepository
        ) : IRequestHandler<CreateSalesOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running CreateSalesOrderCommand");

            var customer = await customerRepository.ExistsAsync(request.IdCustomer);

            if (!customer) throw new NotFoundException("customer not found");

            salesOrderRepository.Order = new SalesOrder
            {
                IdCustomer = request.IdCustomer,
                Date = request.Date,
                //DueDate = request.DueDate,
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "",
                SubTotal = 0,
                DiscountPercentaje = 0,
                DiscountTotal = 0,
                Total = 0,
                Zone = request.Zone,
                //ConfirmedAt = request.ConfirmedAt,
                //CanceledAt = request.CanceledAt,
            };

            await salesOrderRepository.CreateAsync();

            return salesOrderRepository.Order.Id;
        }
    }
}

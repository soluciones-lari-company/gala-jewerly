using JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.AddPaymentToSO
{
    public class AddPaymentToSOCommand: IRequest<Guid>
    {
        public Guid SalesOrderId { get; set; }
        public Guid IdReceivingAccount { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class AddPaymentToSOCommandHandler(
        ILogger<SaleOrderStep3PaymentCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        ISalePaymentRepository paymentRepository
        ) : IRequestHandler<AddPaymentToSOCommand, Guid>
    {
        public async Task<Guid> Handle(AddPaymentToSOCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running SaleOrderStep3PaymentCommand");

            var order = await salesOrderRepository.GetByIdAsync(request.SalesOrderId);

            #region validate order
            if (!order)
                throw new NotFoundException("sales order not found");

            if (salesOrderRepository.Order.CanceledAt != null)
                throw new InvalidOperationException("sales order canceled");

            if (salesOrderRepository.Order.ConfirmedAt != null)
                throw new InvalidOperationException("sales order has been confirmed");

            if (salesOrderRepository.Order.Total <= 0 || salesOrderRepository.Order.SaleOrderLinesNavigation.Count() == 0)
                throw new InvalidOperationException("Please add items to this order first");
            #endregion


            paymentRepository.SalePayment = new SalePayment
            {
                Date = DateTime.UtcNow,
                PaymentMethod = request.PaymentMethod,
                Total = request.Total,
                IdAccount = request.IdReceivingAccount,
                IdCustomer = salesOrderRepository.Order.IdCustomer,
                IdSaleOrder = request.SalesOrderId,
                Customer = null,
                Account = null,
            };

            var result = await paymentRepository.CreateAsync();

            return result;
        }
    }
}

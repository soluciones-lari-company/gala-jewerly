using JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.DeletePaymentToSO
{
    public class DeletePaymentToSOCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
        public Guid PaymentId { get; set; }
    }

    public class DeletePaymentToSOCommandHandler(
        ILogger<SaleOrderStep3PaymentCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        ISalePaymentRepository paymentRepository
        ) : IRequestHandler<DeletePaymentToSOCommand>
    {
        public async Task Handle(DeletePaymentToSOCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running DeletePaymentToSOCommand");

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

            var payment = salesOrderRepository.Order.PaymentsNavigation.FirstOrDefault(e => e.Id == request.PaymentId);

            if(payment == null)
            {
                throw new NotFoundException("payment not found");
            }

            paymentRepository.Remove(payment);
        }
    }
}

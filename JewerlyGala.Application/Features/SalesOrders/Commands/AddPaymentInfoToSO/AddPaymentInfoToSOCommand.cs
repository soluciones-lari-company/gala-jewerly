using JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment;
using JewerlyGala.Domain.Constans;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.AddPaymentInfoToSO
{
    public class AddPaymentInfoToSOCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
        /// <summary>
        /// PUE
        /// PPD
        /// </summary>
        public string PaymentTerms { get; set; } = string.Empty;
        public string PaymentConditions { get; set; } = string.Empty;
    }

    public class AddPaymentInfoToSOCommandHandler(
                ILogger<SaleOrderStep3PaymentCommandHandler> logger,
                ISalesOrderRepository salesOrderRepository,
                ISalePaymentRepository paymentRepository
        ) : IRequestHandler<AddPaymentInfoToSOCommand>
    {
        public async Task Handle(AddPaymentInfoToSOCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running AddPaymentInfoToSOCommand");

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

            #region validate payment terms
            var paymentTerms = typeof(PaymentTerms).GetField(request.PaymentTerms.ToUpper(), BindingFlags.Static | BindingFlags.Public);

            if (paymentTerms == null)
                throw new InvalidParamException("Payment Terms invalid");
            #endregion

            if (salesOrderRepository.Order.PaymentTerms != request.PaymentTerms.ToUpper())
            {
                if (salesOrderRepository.Order.PaymentsNavigation.Count > 0)
                {
                    paymentRepository.RemoveRange(salesOrderRepository.Order.PaymentsNavigation);
                }
            }

            if (request.PaymentTerms.ToUpper() == PaymentTerms.PPD)
            {
                var paymentConditions = typeof(PaymentConditions).GetField(request.PaymentConditions.ToUpper(), BindingFlags.Static | BindingFlags.Public);
                if (paymentConditions == null)
                    throw new InvalidParamException("Payment Conditions invalid");

                if (request.PaymentConditions.ToUpper() == PaymentConditions.NET07)
                    salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(7);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET10)
                    salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(10);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET15)
                    salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(15);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET30)
                    salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(30);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET60)
                    salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(60);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET90)
                    salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(90);

                salesOrderRepository.Order.PaymentConditions = request.PaymentConditions.ToUpper();

            }
            else
            {
                salesOrderRepository.Order.PaymentConditions = PaymentConditions.NET00;
            }

            salesOrderRepository.Order.PaymentTerms = request.PaymentTerms.ToUpper();
            salesOrderRepository.Order.PaymentMethod = PaymentMethods.Pordefinir;
            
            await salesOrderRepository.UpdateAsync();
        }
    }
}

using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Constans;
using System.Reflection;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment
{
    public class SaleOrderStep3PaymentCommand : IRequest
    {
        public Guid SalesOrderId { get; set; }
        public string PaymentTerms { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentConditions { get; set; } = string.Empty;
    }

    public class SaleOrderStep3PaymentCommandHandler(
        ILogger<SaleOrderStep3PaymentCommandHandler> logger,
        ISalesOrderRepository salesOrderRepository,
        IItemSerieRepository itemSerieRepository
        ) : IRequestHandler<SaleOrderStep3PaymentCommand>
    {
        public async Task Handle(SaleOrderStep3PaymentCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running SaleOrderStep3PaymentCommand");

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

            if (salesOrderRepository.Order.Total <= 0 || salesOrderRepository.Order.SaleOrderLinesNavigation.Count() == 0)
            {
                throw new InvalidParamException("Please add items to this order first");
            }

            var paymentTerms = typeof(PaymentTerms).GetField(request.PaymentTerms.ToUpper(), BindingFlags.Static | BindingFlags.Public);
            if(paymentTerms == null) throw new InvalidParamException("Payment Terms invalid");

            //var paymentMethod = typeof(PaymentMethods).GetField(request.PaymentMethod.ToUpper(), BindingFlags.Static | BindingFlags.Public);
            //if (paymentMethod == null) throw new InvalidParamException("payment Method invalid");

            if(request.PaymentTerms.ToUpper() == PaymentTerms.PPD)
            {
                var paymentConditions = typeof(PaymentConditions).GetField(request.PaymentConditions.ToUpper(), BindingFlags.Static | BindingFlags.Public);
                if (paymentConditions == null) throw new InvalidParamException("Payment Conditions invalid");


                //if (request.PaymentConditions.ToUpper() == PaymentConditions.NET00) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(7);
                if (request.PaymentConditions.ToUpper() == PaymentConditions.NET07) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(7);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET10) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(10);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET15) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(15);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET30) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(30);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET60) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(60);
                else if (request.PaymentConditions.ToUpper() == PaymentConditions.NET90) salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(90);

            }
            salesOrderRepository.Order.DueDate = salesOrderRepository.Order.Date.AddDays(7);
            salesOrderRepository.Order.PaymentTerms = request.PaymentTerms.ToUpper();
            salesOrderRepository.Order.PaymentMethod = request.PaymentMethod.ToUpper();
            salesOrderRepository.Order.PaymentConditions = request.PaymentConditions.ToUpper();


            await salesOrderRepository.UpdateAsync();
        }
    }
}

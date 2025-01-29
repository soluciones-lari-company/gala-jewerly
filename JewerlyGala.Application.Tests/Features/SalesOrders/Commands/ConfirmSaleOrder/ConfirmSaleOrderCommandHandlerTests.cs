using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories.Sales;
using Microsoft.Extensions.Logging;
using Moq;
using JewerlyGala.Domain.Repositories;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.ConfirmSaleOrder.Tests
{
    [TestFixture()]
    public class ConfirmSaleOrderCommandHandlerTests
    {
        private Mock<ILogger<ConfirmSaleOrderCommandHandler>> loggerMock;
        private Mock<ISalesOrderRepository> salesOrderRepositoryMock;
        private Mock<IItemSerieRepository> itemSerieRepository;

        public ConfirmSaleOrderCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<ConfirmSaleOrderCommandHandler>>();
            salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            itemSerieRepository = new Mock<IItemSerieRepository>();
        }
        [Test()]
        public void Handle_InvalidOperationException_orderConfirmed()
        {
            var command = new ConfirmSaleOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
            };

            var order = SetSaleOrderWithItems(command.SalesOrderId);
            order.ConfirmedAt = DateTime.Now;

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new ConfirmSaleOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepository.Object
                );
            //act
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("sales order has been confirmed"));
        }
        [Test()]
        public void Handle_InvalidOperationException_orderCanceled()
        {
            var command = new ConfirmSaleOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
            };
            var order = SetSaleOrderWithItems(command.SalesOrderId);
            order.CanceledAt = DateTime.Now;

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new ConfirmSaleOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepository.Object
                );
            //act
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("sales order canceled"));
        }
        [Test()]
        public void Handle_thrownNotFoundException_for_orderNotFound()
        {
            var command = new ConfirmSaleOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(false);

            var handler = new ConfirmSaleOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepository.Object
                );

            //act
            var exception = Assert.ThrowsAsync<Domain.Exceptions.NotFoundException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("sales order not found"));
        }

        private SalesOrder SetSaleOrderWithItems(Guid IdOrder)
        {
            #region set order entities
            var order = new SalesOrder
            {
                Id = IdOrder,
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 12),
                PaymentTerms = "PPD",
                PaymentMethod = "",
                PaymentConditions = "NET07",
                SubTotal = 0,
                DiscountPercentaje = 0,
                DiscountTotal = 0,
                Total = 0,
                Zone = "Lira",
                CanceledAt = null,
                ConfirmedAt = DateTime.Now
            };

            var line = new SaleOrderLine();
            line.Id = 1;
            line.SalesOrderId = IdOrder;
            line.NumLine = 0;
            line.ItemSerieId = Guid.NewGuid();
            line.SerieCode = "A300";
            line.Descripcion = "Broqueles medida 2mm en oro 10k";
            line.Quantity = 5;
            line.UnitPrice = 260;
            line.SubTotal = line.Quantity * line.UnitPrice;
            line.DiscountPercentaje = 0;
            line.DiscountTotal = 0;
            line.Total = line.SubTotal - line.DiscountTotal;
            line.UnitPriceFinal = (line.Total / line.Quantity);

            order.SaleOrderLinesNavigation.Add(line);

            order.SubTotal = order.SaleOrderLinesNavigation.Sum(e => e.SubTotal);
            order.DiscountTotal = order.SaleOrderLinesNavigation.Sum(e => e.DiscountTotal);
            order.Total = order.SaleOrderLinesNavigation.Sum(e => e.Total);

            order.PaymentsNavigation = [];

            var payment = new SalePayment
            {
                Id  = Guid.NewGuid(),
                Date = DateTime.Now,
                IdCustomer = order.IdCustomer,
                IdAccount = Guid.NewGuid(),
                Total = order.Total
            };

            order.PaymentsNavigation.Add(payment);

            #endregion

            return order;
        }
    }
}
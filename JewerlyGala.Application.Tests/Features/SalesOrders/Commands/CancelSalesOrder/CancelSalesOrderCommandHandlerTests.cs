using NUnit.Framework;
using JewerlyGala.Application.Features.SalesOrders.Commands.CancelSalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewerlyGala.Application.Features.SalesOrders.Commands.DeleteLineFromOrder;
using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.CancelSalesOrder.Tests
{
    [TestFixture()]
    public class CancelSalesOrderCommandHandlerTests
    {
        private Mock<ILogger<CancelSalesOrderCommandHandler>> loggerMock;
        private Mock<ISalesOrderRepository> salesOrderRepositoryMock;
        private Mock<IItemSerieRepository> itemSerieRepositoryMock;

        public CancelSalesOrderCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<CancelSalesOrderCommandHandler>>();
            salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            itemSerieRepositoryMock = new Mock<IItemSerieRepository>();
        }

        [Test()]
        public void Handle_InvalidOperationException_orderCanceled()
        {
            var command = new CancelSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
            };
            var order = new SalesOrder
            {
                Id = Guid.NewGuid(),
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 12),
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "",
                SubTotal = 0,
                DiscountPercentaje = 0,
                DiscountTotal = 0,
                Total = 0,
                Zone = "Lira",
                CanceledAt = DateTime.Now,
            };
            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new CancelSalesOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );
            //act
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
        }

        [Test()]
        public void Handle_thrownNotFoundException_for_orderNotFound()
        {
            var command = new CancelSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid()
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(false);

            var handler = new CancelSalesOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );

            //act
            var exception = Assert.ThrowsAsync<Domain.Exceptions.NotFoundException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
        }
    }
}
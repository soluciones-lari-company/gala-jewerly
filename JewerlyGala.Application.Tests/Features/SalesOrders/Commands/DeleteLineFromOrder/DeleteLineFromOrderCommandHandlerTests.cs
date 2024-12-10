using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.DeleteLineFromOrder.Tests
{
    [TestFixture()]
    public class DeleteLineFromOrderCommandHandlerTests
    {
        private Mock<ILogger<DeleteLineFromOrderCommandHandler>> loggerMock;
        private Mock<ISalesOrderRepository> salesOrderRepositoryMock;
        private Mock<IItemSerieRepository> itemSerieRepositoryMock;

        public DeleteLineFromOrderCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<DeleteLineFromOrderCommandHandler>>();
            salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            itemSerieRepositoryMock = new Mock<IItemSerieRepository>();
        }

        [Test()]
        public void Handle_InvalidOperationException_orderConfirmed()
        {
            var command = new DeleteLineFromOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                Quantity = 1
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
                CanceledAt = null,
                ConfirmedAt = DateTime.Now
            };
            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new DeleteLineFromOrderCommandHandler(
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
        public void Handle_InvalidOperationException_orderCanceled()
        {
            var command = new DeleteLineFromOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                Quantity = 1
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

            var handler = new DeleteLineFromOrderCommandHandler(
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
            var command = new DeleteLineFromOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                Quantity = 1
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(false);

            var handler = new DeleteLineFromOrderCommandHandler(
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
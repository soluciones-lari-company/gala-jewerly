using JewerlyGala.Domain.Repositories.Sales;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.CreateSalesOrder.Tests
{
    [TestFixture()]
    public class CreateSalesOrderCommandHandlerTests
    {
        [Test()]
        public async Task Hande_return_orderId_created()
        {
            var loggerMock = new Mock<ILogger<CreateSalesOrderCommandHandler>>();
            var salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            // arrange
            var command = new CreateSalesOrderCommand
            {
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 6),
                Zone = "Lira"
            };

            var order = new SalesOrder
            {
                Id = Guid.NewGuid(),
                IdCustomer = command.IdCustomer,
                Date = command.Date,
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "",
                SubTotal = 0,
                DiscountPercentaje = 0,
                DiscountTotal = 0,
                Total = 0,
                Zone = command.Zone,
            };
            customerRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            salesOrderRepositoryMock.Setup(repo => repo.Order)
            .Returns(order);

            salesOrderRepositoryMock.Setup(repo => repo.CreateAsync())
                .ReturnsAsync(order.Id);

            var handler = new CreateSalesOrderCommandHandler(loggerMock.Object, salesOrderRepositoryMock.Object, customerRepositoryMock.Object);

            var result = await handler.Handle(command, default);
            result.Should().NotBeEmpty();
        }

        [Test()]
        public void Handle_thrownNotFoundException_for_customerNotFound()
        {
            var loggerMock = new Mock<ILogger<CreateSalesOrderCommandHandler>>();
            var salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            // arrange
            var command = new CreateSalesOrderCommand
            {
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 6),
                Zone = "Lira"
            };

            customerRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            var handler = new CreateSalesOrderCommandHandler(loggerMock.Object, salesOrderRepositoryMock.Object, customerRepositoryMock.Object);

            // act
            ////result.Should().NotBeEmpty();
            var exception = Assert.ThrowsAsync<Domain.Exceptions.NotFoundException>(async () =>
            {
                await handler.Handle(command, default);
            });

            //// Assert       
            //Assert.IsNotNull(exception);

            //var result = await handler.Handle(command, default);
            //result.Should().NotBeEmpty();
        }
    }
}
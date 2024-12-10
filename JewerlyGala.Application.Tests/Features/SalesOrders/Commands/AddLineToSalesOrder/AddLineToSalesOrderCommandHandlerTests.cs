using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;
using JewerlyGala.Domain.Repositories.Sales;
using Moq;
using JewerlyGala.Domain.Entities;
using FluentAssertions;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder.Tests
{
    [TestFixture()]
    public class AddLineToSalesOrderCommandHandlerTests
    {
        private Mock<ILogger<AddLineToSalesOrderCommandHandler>> loggerMock;
        private Mock<ISalesOrderRepository> salesOrderRepositoryMock;
        private Mock<IItemSerieRepository> itemSerieRepositoryMock;

        public AddLineToSalesOrderCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<AddLineToSalesOrderCommandHandler>>();
            salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            itemSerieRepositoryMock = new Mock<IItemSerieRepository>();
        }
        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task Handle_successfullExistsLine(decimal discountPercentaje)
        {
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
                Quantity = 2
            };
            decimal subtotalOrder = 1300;
            decimal orderDisacount = discountPercentaje > 0 ? (1300 * (discountPercentaje / 100)) : 0;
            var order = new SalesOrder
            {
                Id = command.SalesOrderId,
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 12),
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "",
                SubTotal = subtotalOrder,
                DiscountPercentaje = discountPercentaje,
                DiscountTotal = orderDisacount,
                Total = subtotalOrder - orderDisacount,
                Zone = "Lira",
                CanceledAt = null,
                ConfirmedAt = null
            };
            order.SaleOrderLinesNavigation.Add(new SaleOrderLine
            {
                Id = 1,
                SalesOrderId = command.SalesOrderId,
                NumLine = 0,
                ItemSerieId = command.ItemSerieId,
                SerieCode = "A300",
                Descripcion = "Broqueles medida 2mm en oro 10k",
                Quantity = 5,
                UnitPrice = 260,
                SubTotal = subtotalOrder,
                DiscountPercentaje = discountPercentaje,
                DiscountTotal = orderDisacount,
                Total = subtotalOrder - orderDisacount,
                UnitPriceFinal = (subtotalOrder - orderDisacount / 5),
            });
            var serie = new ItemSerie
            {
                Id = Guid.NewGuid(),
                SerieCode = "A300",
                Description = "Broqueles medida 2mm en oro 10k",
                MaterialId = 0,
                Quantity = 10,
                QuantitySold = 0,
                QuantityCommited = 5,
                QuantityFree = 5,
                SupplierId = Guid.NewGuid(),
                PurchaseUnitMeasure = "Item",
                PurchasePriceByUnitMeasure = 0,
                PurchaseDate = new DateOnly(2024, 12, 12),
                PurchaseUnitPrice = 100,
                SalePercentRentability = 260,
                SaleUnitPrice = 260,
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);
            itemSerieRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ItemSerieId)).ReturnsAsync(serie);

            var handler = new AddLineToSalesOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );

            decimal disacountTotal = discountPercentaje > 0 ? (1820 * (discountPercentaje / 100)) : 0;

            var result = await handler.Handle(command, default);
            order.SubTotal.Should().Be(1820);
            order.DiscountPercentaje.Should().Be(discountPercentaje);
            order.DiscountTotal.Should().Be(disacountTotal);
            order.Total.Should().Be(1820 - disacountTotal);
            serie.QuantityFree.Should().Be(3);
            serie.QuantitySold.Should().Be(0);
            serie.QuantityCommited.Should().Be(7);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task Handle_successfullAddNewLine(decimal discountPercentaje)
        {
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
                Quantity = 5
            };
            var order = new SalesOrder
            {
                Id = command.SalesOrderId,
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 12),
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "",
                SubTotal = 0,
                DiscountPercentaje = discountPercentaje,
                DiscountTotal = 0,
                Total = 0,
                Zone = "Lira",
                CanceledAt = null,
                ConfirmedAt = null
            };
            order.SaleOrderLinesNavigation.Add(new SaleOrderLine
            {

            });
            var serie = new ItemSerie
            {
                Id = Guid.NewGuid(),
                SerieCode = "A300",
                Description = "Broqueles medida 2mm en oro 10k",
                MaterialId = 0,
                Quantity = 10,
                QuantitySold = 0,
                QuantityCommited = 0,
                QuantityFree = 10,
                SupplierId = Guid.NewGuid(),
                PurchaseUnitMeasure = "Item",
                PurchasePriceByUnitMeasure = 0,
                PurchaseDate = new DateOnly(2024, 12, 12),
                PurchaseUnitPrice = 100,
                SalePercentRentability = 260,
                SaleUnitPrice = 260,
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);
            itemSerieRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ItemSerieId)).ReturnsAsync(serie);

            var handler = new AddLineToSalesOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );

            decimal disacountTotal = discountPercentaje > 0 ? (1300 * (discountPercentaje / 100)) : 0;

            var result = await handler.Handle(command, default);
            order.SubTotal.Should().Be(1300);
            order.DiscountPercentaje.Should().Be(discountPercentaje);
            order.DiscountTotal.Should().Be(disacountTotal);
            order.Total.Should().Be(1300 - disacountTotal);
            serie.QuantityFree.Should().Be(5);
            serie.QuantitySold.Should().Be(0);
            serie.QuantityCommited.Should().Be(5);
        }

        [Test()]
        public void Handle_InvalidOperationException_unavailableQuantity()
        {
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
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
                ConfirmedAt = null
            };
            var serie = new ItemSerie
            {
                Id = command.ItemSerieId,
                QuantityFree = 0
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);
            itemSerieRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ItemSerieId)).ReturnsAsync(serie);

            var handler = new AddLineToSalesOrderCommandHandler(
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
        public void Handle_NotFoundException_serirNotFound()
        {
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
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
                ConfirmedAt = null
            };
            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);
            itemSerieRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ItemSerieId)).ReturnsAsync((ItemSerie)null);

            var handler = new AddLineToSalesOrderCommandHandler(
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

        [Test()]
        public void Handle_InvalidOperationException_orderConfirmed()
        {
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
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

            var handler = new AddLineToSalesOrderCommandHandler(
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
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
                Quantity = 1
            };
            var order = new SalesOrder
            {
                Id = Guid.NewGuid(),
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024,12,12),
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

            var handler = new AddLineToSalesOrderCommandHandler(
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
            var command = new AddLineToSalesOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                NumLine = 1,
                Quantity = 1
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(false);

            var handler = new AddLineToSalesOrderCommandHandler(
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
using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder;
using JewerlyGala.Domain.Entities;
using FluentAssertions;

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

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task Handle_success_deletePartialLine(decimal discountPercentaje)
        {
            var command = new DeleteLineFromOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                Quantity = 2
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

            var line = new SaleOrderLine();
            line.Id = 1;
            line.SalesOrderId = command.SalesOrderId;
            line.NumLine = 0;
            line.ItemSerieId = command.ItemSerieId;
            line.SerieCode = "A300";
            line.Descripcion = "Broqueles medida 2mm en oro 10k";
            line.Quantity = 5;
            line.UnitPrice = 260;
            line.SubTotal = line.Quantity * line.UnitPrice;
            line.DiscountPercentaje = discountPercentaje;
            line.DiscountTotal = discountPercentaje > 0 ? (line.SubTotal * (order.DiscountPercentaje / 100)) : 0;
            line.Total = line.SubTotal - line.DiscountTotal;
            line.UnitPriceFinal = (line.Total / line.Quantity);

            order.SaleOrderLinesNavigation.Add(line);

            order.SubTotal = order.SaleOrderLinesNavigation.Sum(e => e.SubTotal);
            order.DiscountTotal = order.SaleOrderLinesNavigation.Sum(e => e.DiscountTotal);
            order.Total = order.SaleOrderLinesNavigation.Sum(e => e.Total);

            var serie = new ItemSerie
            {
                Id = command.ItemSerieId,
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

            var handler = new DeleteLineFromOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );
            //act
            await handler.Handle(command, default);

            decimal disacountTotal = discountPercentaje > 0 ? (780 * (discountPercentaje / 100)) : 0;
            // Assert       
            order.SubTotal.Should().Be(780);
            order.DiscountTotal.Should().Be(disacountTotal);
            order.Total.Should().Be(780 - disacountTotal);
            serie.QuantityCommited.Should().Be(3);
            serie.QuantityFree.Should().Be(7);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task Handle_success_deleteEntireLine(decimal discountPercentaje)
        {
            var command = new DeleteLineFromOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                Quantity = 5
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
                Id = command.ItemSerieId,
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

            var handler = new DeleteLineFromOrderCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );
            //act
            await handler.Handle(command, default);

            // Assert       
            order.SubTotal.Should().Be(0);
            order.Total.Should().Be(0);
            order.SaleOrderLinesNavigation.FirstOrDefault(e => e.ItemSerieId == command.ItemSerieId).Should().Be(null);
            serie.QuantityCommited.Should().Be(0);
            serie.QuantityFree.Should().Be(10);
        }

        [Test()]
        public void Handle_NotFoundException_lineNotFound()
        {
            var command = new DeleteLineFromOrderCommand
            {
                SalesOrderId = Guid.NewGuid(),
                ItemSerieId = Guid.NewGuid(),
                Quantity = 1
            };
            decimal discountPercentaje = 0;
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

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

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
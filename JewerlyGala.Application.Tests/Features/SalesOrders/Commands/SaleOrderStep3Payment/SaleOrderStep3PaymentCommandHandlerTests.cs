using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;

namespace JewerlyGala.Application.Features.SalesOrders.Commands.SaleOrderStep3Payment.Tests
{
    [TestFixture()]
    public class SaleOrderStep3PaymentCommandHandlerTests
    {
        private Mock<ILogger<SaleOrderStep3PaymentCommandHandler>> loggerMock;
        private Mock<ISalesOrderRepository> salesOrderRepositoryMock;
        private Mock<IItemSerieRepository> itemSerieRepositoryMock;

        public SaleOrderStep3PaymentCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<SaleOrderStep3PaymentCommandHandler>>();
            salesOrderRepositoryMock = new Mock<ISalesOrderRepository>();
            itemSerieRepositoryMock = new Mock<IItemSerieRepository>();
        }

        [Test()]
        public void Handle_InvalidOperationException_noPaymentsAdded()
        {
            var command = new SaleOrderStep3PaymentCommand
            {
                SalesOrderId = Guid.NewGuid(),
                PaymentTerms = "PPD",
                PaymentMethod = "99",
                PaymentConditions = "NET07"
            };

            #region set order entities
            var order = new SalesOrder
            {
                Id = command.SalesOrderId,
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
                ConfirmedAt = null
            };

            var line = new SaleOrderLine();
            line.Id = 1;
            line.SalesOrderId = command.SalesOrderId;
            line.NumLine = 0;
            line.ItemSerieId = Guid.NewGuid();
            line.SerieCode = "A300";
            line.Descripcion = "Broqueles medida 2mm en oro 10k";
            line.Quantity = 5;
            line.UnitPrice = 260;
            line.SubTotal = line.Quantity * line.UnitPrice;
            line.DiscountPercentaje = 0;
            line.DiscountTotal =  0;
            line.Total = line.SubTotal - line.DiscountTotal;
            line.UnitPriceFinal = (line.Total / line.Quantity);

            order.SaleOrderLinesNavigation.Add(line);

            order.SubTotal = order.SaleOrderLinesNavigation.Sum(e => e.SubTotal);
            order.DiscountTotal = order.SaleOrderLinesNavigation.Sum(e => e.DiscountTotal);
            order.Total = order.SaleOrderLinesNavigation.Sum(e => e.Total);
            #endregion

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new SaleOrderStep3PaymentCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );
            //act
            var exception = Assert.ThrowsAsync<InvalidParamException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("no payments added to the order"));

        }
        [Test()]
        public void Handle_InvalidOperationException_PaymentConditionsinvalid()
        {
            var command = new SaleOrderStep3PaymentCommand
            {
                SalesOrderId = Guid.NewGuid(),
                PaymentTerms = "PPD",
                PaymentMethod = "",
                PaymentConditions = ""
            };
            var order = new SalesOrder
            {
                Id = command.SalesOrderId,
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 12),
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "NET07",
                SubTotal = 0,
                DiscountPercentaje = 0,
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

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new SaleOrderStep3PaymentCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );
            //act
            var exception = Assert.ThrowsAsync<InvalidParamException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Payment Conditions invalid"));
        }
        [Test()]
        public void Handle_InvalidOperationException_paymentTermsInvalid()
        {
            var command = new SaleOrderStep3PaymentCommand
            {
                SalesOrderId = Guid.NewGuid(),
                
            };
            var order = new SalesOrder
            {
                Id = command.SalesOrderId,
                IdCustomer = Guid.NewGuid(),
                Date = new DateOnly(2024, 12, 12),
                PaymentTerms = "",
                PaymentMethod = "",
                PaymentConditions = "NET07",
                SubTotal = 0,
                DiscountPercentaje = 0,
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

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new SaleOrderStep3PaymentCommandHandler(
                loggerMock.Object,
                salesOrderRepositoryMock.Object,
                itemSerieRepositoryMock.Object
                );
            //act
            var exception = Assert.ThrowsAsync<InvalidParamException>(async () =>
            {
                await handler.Handle(command, default);
            });

            // Assert       
            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Payment Terms invalid"));
        }
        [Test()]
        public void Handle_InvalidOperationException_orderWithCeroOrNoLines()
        {
            var command = new SaleOrderStep3PaymentCommand
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
                CanceledAt = null,
                ConfirmedAt = null
            };
            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new SaleOrderStep3PaymentCommandHandler(
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
            Assert.That(exception.Message, Is.EqualTo("Please add items to this order first"));
        }
        [Test()]
        public void Handle_InvalidOperationException_orderConfirmed()
        {
            var command = new SaleOrderStep3PaymentCommand
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
                CanceledAt = null,
                ConfirmedAt = DateTime.Now
            };
            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(repo => repo.Order).Returns(order);

            var handler = new SaleOrderStep3PaymentCommandHandler(
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
            Assert.That(exception.Message, Is.EqualTo("sales order has been confirmed"));
        }
        [Test()]
        public void Handle_InvalidOperationException_orderCanceled()
        {
            var command = new SaleOrderStep3PaymentCommand
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

            var handler = new SaleOrderStep3PaymentCommandHandler(
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
            Assert.That(exception.Message, Is.EqualTo("sales order canceled"));
        }
        [Test()]
        public void Handle_thrownNotFoundException_for_orderNotFound()
        {
            var command = new SaleOrderStep3PaymentCommand
            {
                SalesOrderId = Guid.NewGuid(),
            };

            salesOrderRepositoryMock.Setup(repo => repo.GetByIdAsync(command.SalesOrderId)).ReturnsAsync(false);

            var handler = new SaleOrderStep3PaymentCommandHandler(
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
            Assert.That(exception.Message, Is.EqualTo("sales order not found"));
        }
    }
}
using FluentAssertions;
using JewerlyGala.Application.Features.SalesOrders.Commands.CancelSalesOrder;
using JewerlyGala.Domain.Repositories.Accouting;
using Microsoft.Extensions.Logging;
using Moq;

namespace JewerlyGala.Application.Features.Accounts.Commands.UpdateAccount.Tests
{
    [TestFixture()]
    public class UpdateAccountCommandHandlerTests
    {
        private Mock<ILogger<UpdateAccountCommandHandler>> loggerMock;
        private Mock<IAccountRepository> accountRepositoryMock;

        public UpdateAccountCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<UpdateAccountCommandHandler>>();
            accountRepositoryMock = new Mock<IAccountRepository>();
        }

        [Test()]
        public async Task Handle_accountUpdated()
        {
            var command = new UpdateAccountCommand
            {
                Id = Guid.NewGuid(),
                Name = "Gabriela",
                Comments = "cuenta de transferencias",
                IsActive = true,
            };

            var account = new Domain.Entities.Account()
            {
                Id = command.Id,
                Name = "Gabriela transferencias",
                Comments = "cuenta de transferencias",
                IsActive = true,
            };

            accountRepositoryMock.Setup(repo => repo.Account).Returns(account);
            accountRepositoryMock.Setup(repo => repo.GetAsync(command.Id)).ReturnsAsync(true);

            var handler = new UpdateAccountCommandHandler(
                loggerMock.Object,
                accountRepositoryMock.Object
                );

            //act
            await handler.Handle(command, default);

            // Assert
            account.Name.Should().Be("Gabriela");
            //account.LastModified.Should().NotBeNull();
            //account.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        }


        [Test()]
        public void Handle_thrownNotFoundException_customerNotFound()
        {
            var command = new UpdateAccountCommand
            {
                Id = Guid.NewGuid()
            };

            accountRepositoryMock.Setup(repo => repo.GetAsync(command.Id)).ReturnsAsync(false);

            var handler = new UpdateAccountCommandHandler(
                loggerMock.Object,
                accountRepositoryMock.Object
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
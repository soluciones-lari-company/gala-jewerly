using NUnit.Framework;
using JewerlyGala.Application.Features.Accounts.Commands.CreateAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewerlyGala.Application.Features.SalesOrders.Commands.AddLineToSalesOrder;
using JewerlyGala.Domain.Repositories.Sales;
using JewerlyGala.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using JewerlyGala.Domain.Repositories.Accouting;
using JewerlyGala.Application.Features.SalesOrders.Commands.CreateSalesOrder;
using JewerlyGala.Domain.Entities;
using FluentAssertions;

namespace JewerlyGala.Application.Features.Accounts.Commands.CreateAccount.Tests
{
    [TestFixture()]
    public class CreateAccountCommandHandlerTests
    {
        private Mock<ILogger<CreateAccountCommandHandler>> loggerMock;
        private Mock<IAccountRepository> accountRepositoryMock;

        public CreateAccountCommandHandlerTests()
        {
            loggerMock = new Mock<ILogger<CreateAccountCommandHandler>>();
            accountRepositoryMock = new Mock<IAccountRepository>();
        }

        [Test()]
        public async Task Hande_return_orderId_created()
        {
            // arrange
            var command = new CreateAccountCommand
            {
                Name = "Gabriela Atanacio",
                Comments = "Cuenta para transferencias"
            };

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Name = command.Name, 
                Comments = command.Comments,
                IsActive = false
            };

            accountRepositoryMock.Setup(repo => repo.Account)
            .Returns(account);

            accountRepositoryMock.Setup(repo => repo.CreateAsync())
                .ReturnsAsync(account.Id);

            var handler = new CreateAccountCommandHandler(loggerMock.Object, accountRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            result.Should().NotBeEmpty();
        }
    }
}
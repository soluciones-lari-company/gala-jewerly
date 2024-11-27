using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int? Discount { get; set; }
    }

    public class UpdateCustomerCommandHandler(
        ILogger<UpdateCustomerCommandHandler> logger,
        ICustomerRepository customerRepository
        ) : IRequestHandler<UpdateCustomerCommand>
    {
        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Update Customer Command: {request}");

            var existsCustomer = await customerRepository.ExistsAsync(request.CustomerId);

            if (!existsCustomer)
            {
                throw new NotFoundException($"invalid params for {request}");
            }

            var customer = new Customer
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Discount = request.Discount
            };

            await customerRepository.UpdateAsync(request.CustomerId, customer);
        }
    }
}

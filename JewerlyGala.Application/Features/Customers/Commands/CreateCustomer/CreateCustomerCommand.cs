using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int? Discount { get; set; }
    }

    public class CreateCustomerCommandHandler(
        ILogger<CreateCustomerCommandHandler> logger,
        ICustomerRepository customerRepository
        ) : IRequestHandler<CreateCustomerCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Create Customer Command: {request}");

            var customer = new Customer
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Discount = request.Discount
            };

            var proveedorId = await customerRepository.CreateAsync(customer);

            return proveedorId;
        }
    }
}

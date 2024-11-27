using MediatR;
using JewerlyGala.Application.Features.Customers.Commands.UpdateCustomer;
using JewerlyGala.Domain.Repositories.Sales;
using Microsoft.Extensions.Logging;
using AutoMapper;
using JewerlyGala.Application.Features.Customers.DTOs;

namespace JewerlyGala.Application.Features.Customers.Queries.GetAllCustomer
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<CustomerDTO>>
    {
    }

    public class GetAllCustomerQueryHandler(
        ILogger<UpdateCustomerCommandHandler> logger,
        ICustomerRepository customerRepository,
        IMapper mapper
        ) :  IRequestHandler<GetAllCustomerQuery, IEnumerable<CustomerDTO>>
    {
        public async Task<IEnumerable<CustomerDTO>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetAllCustomerQuery");
            var customers = await customerRepository.GetAllAsync();

            return mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }
    }
}

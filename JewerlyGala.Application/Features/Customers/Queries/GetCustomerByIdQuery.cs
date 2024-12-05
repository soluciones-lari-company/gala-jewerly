using AutoMapper;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetAllItemSeries;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Domain.Repositories.Sales;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Customers.Queries
{
    public class GetCustomerByIdQuery: IRequest<CustomerDTO>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetCustomerByIdQueryHandler(
        ILogger<GetAllItemSeriesQuery> logger,
        IMapper mapper,
        ICustomerRepository customerRepository
        ) : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetCustomerByIdQuery");

            var customer  = await customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null) throw new NotFoundException(nameof(customer));

            return mapper.Map<CustomerDTO>(customer );
        }
    }
}

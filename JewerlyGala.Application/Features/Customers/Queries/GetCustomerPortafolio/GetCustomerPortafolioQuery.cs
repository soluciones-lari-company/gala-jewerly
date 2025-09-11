using AutoMapper;
using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Application.Features.Customers.DTOs;
using JewerlyGala.Application.Features.ItemSeries.Queries.GetAllItemSeries;
using JewerlyGala.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.Customers.Queries.GetCustomerPortafolio
{
    public class GetCustomerPortafolioQuery : IRequest<CustomerProfileDTO>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetCustomerPortafolioQueryHandler(
        ILogger<GetAllItemSeriesQuery> logger,
        IMapper mapper,
         IJewerlyDbContext dbContext
        ) : IRequestHandler<GetCustomerPortafolioQuery, CustomerProfileDTO>
    {

        public async Task<CustomerProfileDTO> Handle(GetCustomerPortafolioQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetCustomerPortafolioQuery");

            var customer = await dbContext.Customers.Include(e => e.SalesOrdersNavigation).Include(e => e.Payments).FirstOrDefaultAsync(e => e.Id == request.CustomerId);

            if (customer == null) throw new NotFoundException(nameof(customer));

            return mapper.Map<CustomerProfileDTO>(customer);
        }
    }
}
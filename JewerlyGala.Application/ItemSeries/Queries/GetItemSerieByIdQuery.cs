using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;

namespace JewerlyGala.Application.ItemSeries.Queries
{
    public class GetItemSerieByIdQuery: IRequest<ItemSerie>
    {
        public Guid Id { get; set; }
    }

    public class GetItemSerieByIdQueryHandler(
        IItemSerieRepository itemSerieRepository
        ) : IRequestHandler<GetItemSerieByIdQuery, ItemSerie>
    {
        public async Task<ItemSerie> Handle(GetItemSerieByIdQuery request, CancellationToken cancellationToken)
        {
            var serie = await itemSerieRepository.GetByIdAsync( request.Id );

            if( serie == null )
            {
                throw new NotFoundException($"Item serie not found {request.Id}");
            }

            return serie;
        }
    }
}

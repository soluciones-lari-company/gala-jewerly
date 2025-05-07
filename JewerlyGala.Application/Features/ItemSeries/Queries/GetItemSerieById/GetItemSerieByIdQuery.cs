using AutoMapper;
using JewerlyGala.Application.Common.Security;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;

namespace JewerlyGala.Application.Features.ItemSeries.Queries.GetItemSerieById
{
    [Authorize]
    public class GetItemSerieByIdQuery : IRequest<ItemSerieDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetItemSerieByIdQueryHandler(
        IItemSerieRepository itemSerieRepository,
        IMapper mapper
        ) : IRequestHandler<GetItemSerieByIdQuery, ItemSerieDTO>
    {
        public async Task<ItemSerieDTO> Handle(GetItemSerieByIdQuery request, CancellationToken cancellationToken)
        {
            var serie = await itemSerieRepository.GetByIdAsync(request.Id);

            if (serie == null)
            {
                throw new NotFoundException($"Item serie not found {request.Id}");
            }

            var serieMAp = mapper.Map<ItemSerieDTO>(serie);
            serieMAp.FeatureValues = await itemSerieRepository.GetFeaturesValues(request.Id);
            return serieMAp;
        }
    }
}

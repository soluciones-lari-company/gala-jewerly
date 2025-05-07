using AutoMapper;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;

namespace JewerlyGala.Application.Features.ItemSeries.Queries.GetItemSerieBySerieCode
{
    public class GetItemSerieBySerieCodeQuery : IRequest<ItemSeriePublicDTO>
    {
        public string serieCode { get; set; }
    }

    public class GetItemSerieBySerieCodeQueryHandler(
        IItemSerieRepository itemSerieRepository,
        IMapper mapper
        ) : IRequestHandler<GetItemSerieBySerieCodeQuery, ItemSeriePublicDTO>
    {
        public async Task<ItemSeriePublicDTO> Handle(GetItemSerieBySerieCodeQuery request, CancellationToken cancellationToken)
        {
            var serie = await itemSerieRepository.GetBySerieCodeAsync(request.serieCode);

            if (serie == null)
            {
                throw new NotFoundException($"Item serie not found {request.serieCode}");
            }

            var serieMAp = mapper.Map<ItemSeriePublicDTO>(serie);
            serieMAp.FeatureValues = await itemSerieRepository.GetFeaturesValues(serie.Id);
            return serieMAp;
        }
    }
}

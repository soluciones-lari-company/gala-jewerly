using AutoMapper;
using JewerlyGala.Application.Features.ItemSeries.Common;
using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Queries.GetAllItemSeries
{
    public class GetAllItemSeriesQuery : IRequest<IEnumerable<ItemSerieDTO>>
    {
        public List<ItemSerieFeatures> FeaturesAndValues { get; set; } = [];
    }

    public class GetAllItemSeriesQueryHandler(
        ILogger<GetAllItemSeriesQuery> logger,
        IMapper mapper,
        IItemSerieRepository itemSerieRepository,
        ISearchEngineRepository searchEngineRepository
        ) : IRequestHandler<GetAllItemSeriesQuery, IEnumerable<ItemSerieDTO>>
    {
        public async Task<IEnumerable<ItemSerieDTO>> Handle(GetAllItemSeriesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetAllItemSeriesQuery");

            if(request.FeaturesAndValues != null && request.FeaturesAndValues.Count > 0)
            {
                List<Guid> ids = new List<Guid>();

                foreach (var pattern in request.FeaturesAndValues)
                {
                    var seriesIds = await searchEngineRepository.GetSeriesIdsByFeatureValue(ids, pattern.FeatureName, pattern.Value);

                    ids = seriesIds.ToList();
                }

                ids = ids.Distinct().ToList();

                var series = await itemSerieRepository.GetByMultipleIdsAsync(ids);

                return mapper.Map<IEnumerable<ItemSerieDTO>>(series);
            }

            return [];
        }
    }
}

using JewerlyGala.Application.Features.ItemSeries.DTOs;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Queries.GetFeaturesValues
{
    public class GetFeaturesValuesQuery: IRequest<FeatureValuesDTO>
    {
    }

    public class GetFeaturesValuesQueryHandler(
                ILogger<GetFeaturesValuesQueryHandler> logger,
        ISearchEngineRepository searchEngineRepository
        ) : IRequestHandler<GetFeaturesValuesQuery, FeatureValuesDTO>
    {
        public async Task<FeatureValuesDTO> Handle(GetFeaturesValuesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running GetAllItemSeriesQuery");

            var feau = await searchEngineRepository.GetFeaturesAsync();
            var val = await searchEngineRepository.GetValuesAsync();
            return new FeatureValuesDTO
            {
                Features = feau,
                Values = val,
            };
        }
    }
}

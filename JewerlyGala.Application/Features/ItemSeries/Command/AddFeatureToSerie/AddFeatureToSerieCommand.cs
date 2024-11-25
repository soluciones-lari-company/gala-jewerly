using AutoMapper;
using AutoMapper.Features;
using JewerlyGala.Application.Features.ItemSeries.Common;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Command.AddFeatureToSerie
{
    public class AddFeatureToSerieCommand: IRequest
    {
        public Guid SerieId { get; set; }
        public ItemSerieFeatures Feature { get; set; }
    }

    public class AddFeatureToSerieCommandHandler(
        IMapper mapper,
        ILogger<AddFeatureToSerieCommandHandler> logger,
        IItemSerieRepository itemSerieRepository,
        IItemFeatureValueRepository itemFeatureValueRepository
        ) : IRequestHandler<AddFeatureToSerieCommand>
    {
        public async Task Handle(AddFeatureToSerieCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("runnign AddFeatureToSerieCommand");

            var existsSerie = await itemSerieRepository.ExistsAsync(request.SerieId);

            if (!existsSerie) throw new NotFoundException("Serie not found");

            if(request.Feature == null) throw new ArgumentNullException("feature and value invalid");

            await LinkItemSerieToFeatureAndValue(request.SerieId, request.Feature.FeatureName, request.Feature.Value);
        }

        private async Task LinkItemSerieToFeatureAndValue(Guid serieIdCreated, string featureName, string value)
        {
            // exists feature
            var featureId = await itemFeatureValueRepository.GetFeatureIdByNameAsync(featureName);
            if (featureId == 0)
            {
                featureId = await itemFeatureValueRepository.CreateFeatureAsync(featureName);
            }

            // exists value
            var valueId = await itemFeatureValueRepository.GetFeatureValueIdByNameAsync(value);
            if (valueId == 0)
            {
                valueId = await itemFeatureValueRepository.CreateFeatureValueAsync(value);
            }

            // exists link between feature and value
            var linkdIdForFeatureTovalue = await itemFeatureValueRepository.GetFeatureValueLinkIdAsync(featureId, valueId);
            if (linkdIdForFeatureTovalue == 0)
            {
                linkdIdForFeatureTovalue = await itemFeatureValueRepository.CreateLinkFeatureToValueAsync(featureId, valueId);
            }

            var IsLinkedSerieToFeatureAndValue = await itemFeatureValueRepository.IsFeatureValueLinkedToItemSerieAsync(serieIdCreated, linkdIdForFeatureTovalue);

            if(!IsLinkedSerieToFeatureAndValue)
            {
                await itemFeatureValueRepository.CreateLinkItemSerieToFeatureValueLink(serieIdCreated, linkdIdForFeatureTovalue);
            }
            
        }
    }
}

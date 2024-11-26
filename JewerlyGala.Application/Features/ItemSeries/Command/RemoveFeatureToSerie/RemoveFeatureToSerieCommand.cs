using AutoMapper;
using JewerlyGala.Application.Features.ItemSeries.Common;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Command.RemoveFeatureToSerie
{
    public class RemoveFeatureToSerieCommand: IRequest
    {
        public Guid SerieId { get; set; }
        public ItemSerieFeatures Feature { get; set; }
    }

    public class RemoveFeatureToSerieCommandHandler(
        IMapper mapper,
        ILogger<RemoveFeatureToSerieCommand> logger,
        IItemSerieRepository itemSerieRepository,
        IItemFeatureValueRepository itemFeatureValueRepository
        ) : IRequestHandler<RemoveFeatureToSerieCommand>
    {
        public async Task Handle(RemoveFeatureToSerieCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running RemoveFeatureToSerieCommand");

            var existsSerie = await itemSerieRepository.ExistsAsync(request.SerieId);

            if (!existsSerie) throw new NotFoundException("Serie not found");

            if (request.Feature == null) throw new ArgumentNullException("feature and value invalid");

            await RemoveLinkItemSerieToFeatureAndValue(request.SerieId, request.Feature.FeatureName, request.Feature.Value);
        }

        private async Task RemoveLinkItemSerieToFeatureAndValue(Guid serieIdCreated, string featureName, string value)
        {
            // exists feature
            var featureId = await itemFeatureValueRepository.GetFeatureIdByNameAsync(featureName);
            if (featureId == 0)
            {
                throw new NotFoundException($"Feature [{featureName}] not found");
            }

            // exists value
            var valueId = await itemFeatureValueRepository.GetFeatureValueIdByNameAsync(value);
            if (valueId == 0)
            {
                throw new NotFoundException($"Value [{value}] not found");
            }

            // exists link between feature and value
            var linkdIdForFeatureTovalue = await itemFeatureValueRepository.GetFeatureValueLinkIdAsync(featureId, valueId);
            if (linkdIdForFeatureTovalue != 0)
            {
                await itemFeatureValueRepository.RemoveLinkItemSerieToFeatureValueLink(serieIdCreated, linkdIdForFeatureTovalue);
            }
        }
    }
}

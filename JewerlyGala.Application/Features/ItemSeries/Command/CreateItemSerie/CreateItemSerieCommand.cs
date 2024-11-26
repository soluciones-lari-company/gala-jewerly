using JewerlyGala.Application.Features.ItemSeries.Common;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Command.CreateItemSerie
{
    public class CreateItemSerieCommand : IRequest<Guid>
    {
        public string SerieCode { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Material { get; set; } = default!;
        public int Quantity { get; set; }
        public Guid SupplierId { get; set; }
        public string PurchaseUnitMeasure { get; set; } = default!;
        public decimal PurchasePriceByUnitMeasure { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public decimal PurchaseUnitPrice { get; set; }
        public int SalePercentRentability { get; set; }
        public decimal SaleUnitPrice { get; set; }
        public ICollection<ItemSerieFeatures> FeaturesAndValues { get; set; } = [];
    }

    public class CreateItemSerieCommandHandler(
        ILogger<CreateItemSerieCommandHandler> logger,
        IItemSerieRepository itemSerieRepository,
        IItemMaterialRepository itemMaterialRepository,
        ISupplierRepository supplierRepository,
        IItemFeatureValueRepository itemFeatureValueRepository
        ) : IRequestHandler<CreateItemSerieCommand, Guid>
    {
        public async Task<Guid> Handle(CreateItemSerieCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("creating  new item serie");

            var materialId = await ProcessMaterial(request.Material);

            var supplier = await supplierRepository.GetById(request.SupplierId) ?? throw new NotFoundException($"Suppier: {request.SupplierId} not found");

            var isFreeCode = await itemSerieRepository.IsUsableSerieCodeAsync(request.SerieCode);

            if (!isFreeCode)
            {
                throw new InvalidParamException($"serie code: [{request.SerieCode}] is not available");
            }

            var newSerie = new ItemSerie
            {
                SerieCode = request.SerieCode,
                Description = request.Description,
                MaterialId = materialId,
                Quantity = request.Quantity,
                QuantitySold = 0,
                QuantityCommited = 0,
                QuantityFree = request.Quantity,
                SupplierId = request.SupplierId,
                PurchaseUnitMeasure = request.PurchaseUnitMeasure,
                PurchasePriceByUnitMeasure = request.PurchasePriceByUnitMeasure,
                PurchaseDate = request.PurchaseDate,
                PurchaseUnitPrice = request.PurchaseUnitPrice,
                SalePercentRentability = request.SalePercentRentability,
                SaleUnitPrice = request.SaleUnitPrice,
            };

            var serieIdCreated = await itemSerieRepository.CreateAsync(newSerie);

            await ProcessFeaturesAndValues(serieIdCreated, request.FeaturesAndValues);


            return serieIdCreated;
        }

        private async Task ProcessFeaturesAndValues(Guid serieIdCreated, ICollection<ItemSerieFeatures> featuresAndValues)
        {
            if (serieIdCreated == Guid.Empty)
            {
                logger.LogError("unable to link features and values to itemserie, itemserie could not be saved correctly");
                throw new ArgumentException();
            }


            if (featuresAndValues != null && featuresAndValues.Count > 0)
            {
                foreach (var feature in featuresAndValues)
                {
                    await LinkItemSerieToFeatureAndValue(serieIdCreated, feature.FeatureName, feature.Value);
                }
            }
            else
            {
                logger.LogInformation($"No features and values were sent to link to itemSerie: [{serieIdCreated}]");
            }
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

            await itemFeatureValueRepository.CreateLinkItemSerieToFeatureValueLink(serieIdCreated, linkdIdForFeatureTovalue);
        }

        private async Task<int> ProcessMaterial(string materialName)
        {
            var material = await itemMaterialRepository.GetByName(materialName);

            if (material == null)
            {
                var materialId = await itemMaterialRepository.CreateAsync(materialName);

                return materialId;
            }
            else
            {
                return material.Id;
            }
        }
    }
}

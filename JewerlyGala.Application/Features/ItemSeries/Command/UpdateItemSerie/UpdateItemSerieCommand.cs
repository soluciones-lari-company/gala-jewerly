
using AutoMapper;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JewerlyGala.Application.Features.ItemSeries.Command.UpdateItemSerie
{
    public class UpdateItemSerieCommand : IRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
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
    }

    public class UpdateItemSerieCommandHandler(
        ILogger<UpdateItemSerieCommandHandler> logger,
        IMapper mapper, 
        IItemSerieRepository itemSerieRepository,
        IItemMaterialRepository itemMaterialRepository
        ) : IRequestHandler<UpdateItemSerieCommand>
    {
        public async Task Handle(UpdateItemSerieCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("running UpdateItemSerieCommand");

            var existsSerie = await itemSerieRepository.ExistsAsync(request.Id);

            if (!existsSerie)
                throw new NotFoundException("Serie not found");

            var materialId = await ProcessMaterial(request.Material);

            var serie = new ItemSerie
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

            await itemSerieRepository.UpdateAsync(request.Id, serie);
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

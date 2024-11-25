using AutoMapper;
using JewerlyGala.Application.Features.ItemMaterials.DTOs;
using JewerlyGala.Application.Features.Suppliers.DTOs;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.ItemSeries.DTOs
{
    public class ItemSerieDTO: IMapFrom<ItemSerie>
    {
        public Guid Id { get; set; }
        public string SerieCode { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ItemMaterialDTO Material { get; set; }
        public int Quantity { get; set; }
        public int QuantitySold { get; set; }
        public int QuantityCommited { get; set; }
        public int QuantityFree { get; set; }
        public SupplierDTO Supplier { get; set; }
        public string PurchaseUnitMeasure { get; set; } = default!;
        public decimal PurchasePriceByUnitMeasure { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public decimal PurchaseUnitPrice { get; set; }
        public int SalePercentRentability { get; set; }
        public decimal SaleUnitPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemSerie, ItemSerieDTO>()
                .ForMember(d => d.Material, opt => opt.MapFrom(e => e.ItemMaterialNav))
                .ForMember(d => d.Supplier, opt => opt.MapFrom(e => e.SupplierNav));
        }
    }
}

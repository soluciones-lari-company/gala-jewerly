using AutoMapper;
using JewerlyGala.Application.Features.ItemMaterials.DTOs;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Queries;

namespace JewerlyGala.Application.Features.ItemSeries.DTOs
{
    public class ItemSeriePublicDTO : IMapFrom<ItemSerie>
    {
        public Guid Id { get; set; }
        public string SerieCode { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ItemMaterialDTO Material { get; set; }
        public int QuantityFree { get; set; }
        public decimal SaleUnitPrice { get; set; }
        public ICollection<QItemSerieFeatureValues> FeatureValues { get; set; } = [];
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemSerie, ItemSeriePublicDTO>()
                .ForMember(d => d.Material, opt => opt.MapFrom(e => e.ItemMaterialNav));
        }
    }
}

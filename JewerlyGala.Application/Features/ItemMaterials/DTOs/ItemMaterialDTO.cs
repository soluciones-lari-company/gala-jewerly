using AutoMapper;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Features.ItemMaterials.DTOs
{
    public class ItemMaterialDTO : IMapFrom<ItemMaterial>
    {
        public int Id { get; set; }
        public string MaterialName { get; set; } = default!;
    }
}

using AutoMapper;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Dtos
{
    public class ItemModelDto: IMapFrom<ItemModel>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public ICollection<ItemModelFeatureDto> Features { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemModel, ItemModelDto>()
                .ForMember(d => d.Features, opt => opt.MapFrom(e => e.Features));
        }

        //public static ItemModelDto FromEntity(ItemModel itemModel)
        //{
        //    return new ItemModelDto()
        //    {
        //        Id = itemModel.Id,
        //        Name = itemModel.Name,
        //        Features = itemModel.Features.Select(ItemModelFeatureDto.FromEntity).ToList()
        //    };
        //}
    }
}

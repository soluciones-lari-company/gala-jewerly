using AutoMapper;
using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Dtos
{
    public class ItemModelFeatureDto: IMapFrom<ItemModelFeatureDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual IEnumerable<ItemModelFeatureValueDto> Values { get; set; } = new List<ItemModelFeatureValueDto>();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemModelFeature, ItemModelFeatureDto>()
                .ForMember(d => d.Values, opt => opt.MapFrom(e => e.Values));
        }
        //public static ItemModelFeatureDto FromEntity(ItemModelFeature itemModelFeature)
        //{
        //    return new ItemModelFeatureDto()
        //    {
        //        Id = itemModelFeature.Id,
        //        Name = itemModelFeature.Name,
        //        Values = itemModelFeature.Values.Select(ItemModelFeatureValueDto.FromEntity)
        //    };
        //}
    }
}

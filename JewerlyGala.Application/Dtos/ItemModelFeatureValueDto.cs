using JewerlyGala.Application.Mapping;
using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Application.Dtos
{
    public class ItemModelFeatureValueDto: IMapFrom<ItemModelFeatureValue>
    {
        public int Id { get; set; }
        public string ValueDetails { get; set; } = string.Empty;

        //public static ItemModelFeatureValueDto FromEntity(ItemModelFeatureValue itemModelFeatureValue)
        //{
        //    return new ItemModelFeatureValueDto()
        //    {
        //        Id = itemModelFeatureValue.Id,
        //        ValueDetails = itemModelFeatureValue.ValueDetails,
        //    };
        //}
    }
}

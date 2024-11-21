using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    public class ItemFeatureValueRepository(JewerlyDbContext dbContext) : IItemFeatureValueRepository
    {
        public async Task<int> CreateFeatureAsync(string featureName)
        {
            var feature = new ItemFeature
            {
                FeatureName = featureName,
            };

            dbContext.ItemFeatures.Add(feature);

            await dbContext.SaveChangesAsync();

            return feature.Id;
        }

        public async Task<int> CreateFeatureValueAsync(string valueName)
        {
            var value = new ItemFeatureValue { ValueName = valueName };

            dbContext.ItemFeatureValues.Add(value);

            await dbContext.SaveChangesAsync();

            return value.Id;
        }

        public async Task<int> CreateLinkFeatureToValueAsync(int featureId, int valueId)
        {
            var linkFeatureToValue = new ItemFeatureToValue
            { 
                ValueId = valueId,
                FeatureId = featureId
            };

            dbContext.ItemFeatureToValues.Add(linkFeatureToValue);

            await dbContext.SaveChangesAsync();

            return linkFeatureToValue.Id;
        }

        public async Task CreateLinkItemSerieToFeatureValueLink(Guid itemSerieId, int FeatureToValueId)
        {
            var itemSerieLinkFeatureAndValue = new ItemSerieToFeatureAndValue
            {
                ItemSerieId = itemSerieId,
                ItemFeatureToValueId = FeatureToValueId
            };

            dbContext.ItemSerieToFeatureAndValues.Add(itemSerieLinkFeatureAndValue);

            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetFeatureIdByNameAsync(string featureName)
        {
            var feature = await dbContext.ItemFeatures.FirstOrDefaultAsync( e => e.FeatureName == featureName );
            if(feature == null)
            {
                return 0;
            }
            else
            {
                return feature.Id;
            }
        }

        public async Task<int> GetFeatureValueIdByNameAsync(string valueName)
        {
            var value = await dbContext.ItemFeatureValues.FirstOrDefaultAsync( e => e.ValueName == valueName );
            if (value == null)
            {
                return 0;
            }
            else
            {
                return value.Id;
            }
        }

        public async Task<int> GetFeatureValueLinkIdAsync(int featureId, int valueId)
        {
            var FeatureToValueLink = await dbContext.ItemFeatureToValues
                .FirstOrDefaultAsync(e => e.FeatureId == featureId && e.ValueId == valueId);
            if (FeatureToValueLink == null)
            {
                return 0;
            }
            else
            {
                return FeatureToValueLink.Id;
            }
        }

        public async Task<bool> IsFeatureValueLinkedToItemSerieAsync(Guid itemSerieId, int FeatureToValueId)
        {
            return await dbContext.ItemSerieToFeatureAndValues
                .AnyAsync(e => e.ItemSerieId == itemSerieId && e.ItemFeatureToValueId == FeatureToValueId);
        }

        public async Task RemoveLinkItemSerieToFeatureValueLink(Guid itemSerieId, int FeatureToValueId)
        {
            var link = await dbContext.ItemSerieToFeatureAndValues
                .FirstOrDefaultAsync(e => e.ItemSerieId == itemSerieId && e.ItemFeatureToValueId == FeatureToValueId);

            if(link == null)
            {
                throw new NullReferenceException(nameof(link));
            }

            dbContext.ItemSerieToFeatureAndValues.Remove(link);
            await dbContext.SaveChangesAsync();
        }
    }
}

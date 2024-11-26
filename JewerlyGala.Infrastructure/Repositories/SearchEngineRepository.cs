using JewerlyGala.Domain.Queries;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    public class SearchEngineRepository(JewerlyDbContext dbContext) : ISearchEngineRepository
    {
        private IQueryable<QItemSerieFeatureValues> query = from t01 in dbContext.ItemSerieToFeatureAndValues
                                                            join t02 in dbContext.ItemFeatureToValues on t01.ItemFeatureToValueId equals t02.Id
                                                            join t03 in dbContext.ItemFeatures on t02.FeatureId equals t03.Id
                                                            join t04 in dbContext.ItemFeatureValues on t02.ValueId equals t04.Id
                                                            select new QItemSerieFeatureValues
                                                            {
                                                                SerieId = t01.ItemSerieId,
                                                                Feature = t03.FeatureName,
                                                                Value = t04.ValueName
                                                            };
        public async Task<ICollection<Guid>> GetSeriesIdsByFeatureValue(string feature, string value)
        {
            var results = await query.Where( e=> e.Feature == feature && e.Value == value).Select(e => e.SerieId).ToListAsync();

            return results;
        }

        public async Task<ICollection<Guid>> GetSeriesIdsByFeatureValue(List<Guid> basedItems, string feature, string value)
        {
            if(basedItems.Count() > 0)
            {
                basedItems = basedItems.Distinct().ToList();

                var results = await query.Where(e => e.Feature == feature && e.Value == value && basedItems.Contains(e.SerieId)).Select(e => e.SerieId).ToListAsync();

                return results;
            }
            else
            {
                var results = await query.Where(e => e.Feature == feature && e.Value == value).Select(e => e.SerieId).ToListAsync();

                return results;
            }
        }
    }
}

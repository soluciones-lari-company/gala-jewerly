namespace JewerlyGala.Domain.Repositories
{
    public interface IItemFeatureValueRepository
    {
        public Task<int> CreateFeatureAsync(string featureName);
        public Task<int> CreateFeatureValueAsync(string valueName);
        public Task<int> GetFeatureIdByNameAsync(string featureName);
        public Task<int> GetFeatureValueIdByNameAsync(string valueName);
        public Task<int> GetFeatureValueLinkIdAsync(int featureId, int valueId);
        public Task<int> CreateLinkFeatureToValueAsync(int featureId, int valueId);
        public Task<bool> IsFeatureValueLinkedToItemSerieAsync(Guid itemSerieId, int FeatureToValueId);
        public Task<List<Guid>> GetItemSeriesIdsByFeatureAndValueAsync(int FeatureToValueId);
        public Task CreateLinkItemSerieToFeatureValueLink(Guid itemSerieId, int FeatureToValueId);
        public Task RemoveLinkItemSerieToFeatureValueLink(Guid itemSerieId, int FeatureToValueId);
    }
}

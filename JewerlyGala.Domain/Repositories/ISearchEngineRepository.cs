namespace JewerlyGala.Domain.Repositories
{
    public interface ISearchEngineRepository
    {
        Task<ICollection<string>> GetFeaturesAsync();
        Task<ICollection<string>> GetValuesAsync();
        Task<ICollection<Guid>> GetSeriesIdsByFeatureValue(string feature,  string value);
        Task<ICollection<Guid>> GetSeriesIdsByFeatureValue(List<Guid> basedItems, string feature,  string value);
    }
}

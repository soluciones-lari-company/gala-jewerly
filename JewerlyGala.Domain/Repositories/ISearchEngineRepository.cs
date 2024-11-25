namespace JewerlyGala.Domain.Repositories
{
    public interface ISearchEngineRepository
    {
        Task<ICollection<Guid>> GetSeriesIdsByFeatureValue(string feature,  string value);
        Task<ICollection<Guid>> GetSeriesIdsByFeatureValue(List<Guid> basedItems, string feature,  string value);
    }
}

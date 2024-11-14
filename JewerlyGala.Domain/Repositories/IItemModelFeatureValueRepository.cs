namespace JewerlyGala.Domain.Repositories
{
    public interface IItemModelFeatureValueRepository
    {
        Task<int> CreateAsync(int idFeature, string name);
        Task<int> ExistsByName(string name);
        Task<bool> UpdateAsync(int idFeature, int idValue, string name);
    }
}

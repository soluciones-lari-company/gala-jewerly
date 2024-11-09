using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories
{
    public interface IItemModelsRepository
    {
        Task<IEnumerable<ItemModel>> GetAllItemModelsAsync();
        Task<ItemModel> GetByIdAsync(int id);
        Task<int> CreateAsync(string name);
        Task AddFeatureAsync(int id, string name);
        Task AddValueToFeature(int id, string value);
        Task<bool> UpdateAsync(ItemModel model);
    }
}

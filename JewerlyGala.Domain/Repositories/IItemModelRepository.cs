using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories
{
    public interface IItemModelRepository
    {
        Task<IEnumerable<ItemModel>> GetAllItemModelsAsync();
        Task<ItemModel> GetByIdAsync(int id);
        Task<int> CreateAsync(string name);
        Task<bool> UpdateAsync(int id, string name);
        Task<int> ExistsByName(string name);
        Task GetByIdAsync(object id);
    }
}

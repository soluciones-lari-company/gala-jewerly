using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories
{
    public interface IItemModelsRepository
    {
        Task<IEnumerable<ItemModel>> GetAllItemModelsAsync();
        Task<ItemModel> GetByIdAsync(int id);
        Task<int> Create(string name);
        Task<bool> Update(ItemModel model);
    }
}

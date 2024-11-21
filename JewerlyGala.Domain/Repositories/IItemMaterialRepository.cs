using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories
{
    public interface IItemMaterialRepository
    {
        Task<int> CreateAsync(string name);
        Task<ICollection<ItemMaterial>> GetAllAsync();
        Task<ItemMaterial?> GetByName(string name);
        Task<ItemMaterial?> GetById(int id);
    }
}

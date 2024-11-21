using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories
{
    public interface IItemSerieRepository
    {
        public Task<Guid> CreateAsync(ItemSerie itemSerie);
        public Task<ICollection<ItemSerie>> GetAllAsync();
        public Task<ItemSerie?> GetByIdAsync(Guid id);
        public Task<ItemSerie?> GetBySerieCodeAsync(string serieCod);
        public Task<bool> IsUsableSerieCodeAsync(string serieCode);
         
    }
}

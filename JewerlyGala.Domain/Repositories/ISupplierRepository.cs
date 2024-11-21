using JewerlyGala.Domain.Entities;

namespace JewerlyGala.Domain.Repositories
{
    public interface ISupplierRepository
    {
        Task<Guid> CreateAsync(string name);
        Task<ICollection<Supplier>> GetAllAsync();
        Task<Supplier?> GetByName(string name);
        Task<Supplier?> GetById(Guid id);
    }
}

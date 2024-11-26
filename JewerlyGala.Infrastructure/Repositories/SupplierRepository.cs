using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    public class SupplierRepository(JewerlyDbContext dbContext) : ISupplierRepository
    {
        public async Task<Guid> CreateAsync(string name)
        {
            var supplier = new Supplier
            {
                SupplierName = name,
            };

            dbContext.Suppliers.Add(supplier);

            await dbContext.SaveChangesAsync();

            return supplier.Id;
        }

        public async Task<ICollection<Supplier>> GetAllAsync()
        {
            return await dbContext.Suppliers.OrderBy(e => e.SupplierName).ToListAsync();
        }

        public async Task<Supplier?> GetById(Guid id)
        {
            var supplier = await dbContext.Suppliers.FirstOrDefaultAsync(e => e.Id == id);

            return supplier;
        }

        public async Task<Supplier?> GetByName(string name)
        {
            var supplier = await dbContext.Suppliers.FirstOrDefaultAsync(e => e.SupplierName == name);

            return supplier;
        }
    }
}

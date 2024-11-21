using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    internal class ItemMaterialRepository(JewerlyDbContext dbContext) : IItemMaterialRepository
    {
        public async Task<int> CreateAsync(string name)
        {
            var material = new ItemMaterial
            {
                MaterialName = name,
            };

            dbContext.ItemMaterials.Add(material);

            await dbContext.SaveChangesAsync();

            return material.Id;
        }

        public async Task<ICollection<ItemMaterial>> GetAllAsync()
        {
            return await dbContext.ItemMaterials.OrderBy(e => e.MaterialName).ToListAsync();
        }

        public async Task<ItemMaterial?> GetById(int id)
        {
            var material = await dbContext.ItemMaterials.FirstOrDefaultAsync(e => e.Id == id);

            return material;
        }

        public async Task<ItemMaterial?> GetByName(string name)
        {
            var material = await dbContext.ItemMaterials.FirstOrDefaultAsync(e => e.MaterialName == name);

            return material;
        }
    }
}

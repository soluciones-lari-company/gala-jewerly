using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    public class ItemSerieRepository(JewerlyDbContext dbContext) : IItemSerieRepository
    {
        public async Task<Guid> CreateAsync(ItemSerie itemSerie)
        {
            if(itemSerie == null) 
                throw new ArgumentNullException(nameof(itemSerie));


            dbContext.ItemSeries.Add(itemSerie);

            await dbContext.SaveChangesAsync();


            return itemSerie.Id;
        }

        public async Task<ICollection<ItemSerie>> GetAllAsync()
        {
            var series = await dbContext.ItemSeries.OrderBy(e => e.Created).ToListAsync();

            return series;
        }

        public async Task<ItemSerie?> GetByIdAsync(Guid id)
        {
            return await dbContext.ItemSeries
                .Include(e => e.SupplierNav)
                .Include(e => e.ItemMaterialNav)
                .FirstOrDefaultAsync( e => e.Id == id);
        }

        public async Task<ItemSerie?> GetBySerieCodeAsync(string serieCode)
        {
            return await dbContext.ItemSeries.FirstOrDefaultAsync(e => e.SerieCode == serieCode);
        }

        public async Task<bool> IsUsableSerieCodeAsync(string serieCode)
        {
            return !await dbContext.ItemSeries.AnyAsync(e => e.SerieCode == serieCode);
        }
    }
}

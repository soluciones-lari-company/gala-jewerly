using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Exceptions;
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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await dbContext.ItemSeries.AnyAsync(e => e.Id == id);
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

        public async Task<ICollection<ItemSerie>> GetByMultipleIdsAsync(List<Guid> ids)
        {
            if(ids.Count() > 0)
            {
                var series = await dbContext.ItemSeries
                    .Include(e => e.ItemMaterialNav)
                    .Include(e => e.SupplierNav)
                    .Where(e => ids.Contains(e.Id)).OrderBy(e => e.Created).ToListAsync();

                return series;
            }
            else
            {
                return [];
            }
        }

        public async Task<ItemSerie?> GetBySerieCodeAsync(string serieCode)
        {
            return await dbContext.ItemSeries.FirstOrDefaultAsync(e => e.SerieCode == serieCode);
        }

        public async Task<bool> IsUsableSerieCodeAsync(string serieCode)
        {
            return !await dbContext.ItemSeries.AnyAsync(e => e.SerieCode == serieCode);
        }

        public async Task UpdateAsync(Guid id, ItemSerie itemSerie)
        {
            var serie = await dbContext.ItemSeries
                .FirstOrDefaultAsync(e => e.Id == id);

            if (serie == null)
                throw new NullReferenceException(nameof(id));

            serie.SerieCode = itemSerie.SerieCode;
            serie.Description = itemSerie.Description;
            serie.MaterialId = itemSerie.MaterialId;
            serie.Quantity = itemSerie.Quantity;
            serie.QuantitySold = itemSerie.QuantitySold;
            serie.QuantityCommited = itemSerie.QuantityCommited;
            serie.QuantityFree = itemSerie.QuantityFree;
            serie.SupplierId = itemSerie.SupplierId;
            serie.PurchaseUnitMeasure = itemSerie.PurchaseUnitMeasure;
            serie.PurchasePriceByUnitMeasure = itemSerie.PurchasePriceByUnitMeasure;
            serie.PurchaseDate = itemSerie.PurchaseDate;
            serie.PurchaseUnitPrice = itemSerie.PurchaseUnitPrice;
            serie.SalePercentRentability = itemSerie.SalePercentRentability;
            serie.SaleUnitPrice = itemSerie.SaleUnitPrice;

            await dbContext.SaveChangesAsync();
        }
    }
}

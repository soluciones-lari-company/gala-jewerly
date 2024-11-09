using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    public class ItemModelsRepository : IItemModelsRepository
    {
        private JewerlyDbContext dbContext;

        public ItemModelsRepository()
        {
        }

        public ItemModelsRepository(JewerlyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddFeatureAsync(int id, string name)
        {
            throw new NotImplementedException();
        }

        public Task AddValueToFeature(int id, string value)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemModel>> GetAllItemModelsAsync()
        {
            var models = await this.dbContext.ItemModels.ToListAsync();

            return models;
        }

        public async Task<ItemModel?> GetByIdAsync(int id)
        {
            var model = await this.dbContext.ItemModels
                .Include(x => x.Features)
                .ThenInclude(x => x.Values)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(model == null)
            {
                return null;
            }

            return model;
        }

        public async Task<bool> Update(ItemModel model)
        {
            if(model == null)
            {
                throw new NullReferenceException(nameof(model));
            }



            return true;
        }

        public Task<bool> UpdateAsync(ItemModel model)
        {
            throw new NotImplementedException();
        }
    }
}

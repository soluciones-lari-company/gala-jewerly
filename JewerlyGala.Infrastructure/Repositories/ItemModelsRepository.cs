using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Repositories;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Repositories
{
    public class ItemModelsRepository : IItemModelRepository
    {
        private JewerlyDbContext dbContext;

        public ItemModelsRepository(JewerlyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAsync(string name)
        {
            var newitemModel = new ItemModel { Name = name };

            dbContext.ItemModels.Add(newitemModel);

            await dbContext.SaveChangesAsync();

            return newitemModel.Id;
        }

        public async Task<int> ExistsByName(string name)
        {
            var model = await this.dbContext.ItemModels
               .FirstOrDefaultAsync(x => x.Name == name);

            if (model == null)
            {
                return 0;
            }

            return model.Id;
        }

        public async Task<IEnumerable<ItemModel>> GetAllItemModelsAsync()
        {
            var models = await this.dbContext.ItemModels.ToListAsync();

            return models;
        }

        public async Task<ItemModel?> GetByIdAsync(int id)
        {
            var model = await this.dbContext.ItemModels
                .FirstOrDefaultAsync(x => x.Id == id);

            if(model == null)
            {
                return null;
            }

            return model;
        }

        public Task GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            var model = await dbContext.ItemModels
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            model.Name = name;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}

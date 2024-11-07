using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JewerlyGala.Infrastructure.Persistence
{
    public class JewerlyDbContext: DbContext
    {
        internal DbSet<ItemModel> Models => Set<ItemModel>();
        internal DbSet<ItemModelFeature> ItemModelFeatures => Set<ItemModelFeature>();
        internal DbSet<ItemModelFeatureValue> ItemModelFeatureValues => Set<ItemModelFeatureValue>();

        public JewerlyDbContext(DbContextOptions<JewerlyDbContext> options) : base(options) 
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

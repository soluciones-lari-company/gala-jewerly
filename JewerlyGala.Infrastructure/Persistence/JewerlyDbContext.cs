using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Identity;
using JewerlyGala.Infrastructure.Persistence.Intereptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JewerlyGala.Infrastructure.Persistence
{
    public class JewerlyDbContext: IdentityDbContext<User>
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;


        internal DbSet<ItemModel> ItemModels => Set<ItemModel>();
        internal DbSet<ItemModelFeature> ItemModelFeatures => Set<ItemModelFeature>();
        internal DbSet<ItemModelFeatureValue> ItemModelFeatureValues => Set<ItemModelFeatureValue>();

        public JewerlyDbContext(
            DbContextOptions<JewerlyDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
            ) : base(options) 
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

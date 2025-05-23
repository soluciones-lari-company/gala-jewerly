using JewerlyGala.Application.Common.Interfaces;
using JewerlyGala.Domain.Entities;
using JewerlyGala.Domain.Identity;
using JewerlyGala.Infrastructure.Persistence.Intereptors;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JewerlyGala.Infrastructure.Persistence
{
    public class JewerlyDbContext : IdentityDbContext<User>, IJewerlyDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        #region Inventory
        public DbSet<ItemModel> ItemModels => Set<ItemModel>();
        public DbSet<ItemModelFeature> ItemModelFeatures => Set<ItemModelFeature>();
        public DbSet<ItemModelFeatureValue> ItemModelFeatureValues => Set<ItemModelFeatureValue>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<ItemMaterial> ItemMaterials => Set<ItemMaterial>();
        public DbSet<ItemSerie> ItemSeries => Set<ItemSerie>();
        public DbSet<ItemFeature> ItemFeatures => Set<ItemFeature>();
        public DbSet<ItemFeatureValue> ItemFeatureValues => Set<ItemFeatureValue>();
        public DbSet<ItemFeatureToValue> ItemFeatureToValues => Set<ItemFeatureToValue>();
        public DbSet<ItemSerieToFeatureAndValue> ItemSerieToFeatureAndValues => Set<ItemSerieToFeatureAndValue>();
        #endregion

        #region Sales
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SaleOrderLine> SalesOrderLines => Set<SaleOrderLine>();
        #endregion

        #region Accouting
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<SalePayment> SalePayments => Set<SalePayment>();
        #endregion

        public JewerlyDbContext(
            IMediator mediator,
            DbContextOptions<JewerlyDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
        ) : base(options)
        {
            _mediator = mediator;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

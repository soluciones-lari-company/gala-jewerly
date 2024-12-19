﻿using JewerlyGala.Domain.Entities;
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

        #region Inventory
        internal DbSet<ItemModel> ItemModels => Set<ItemModel>();
        internal DbSet<ItemModelFeature> ItemModelFeatures => Set<ItemModelFeature>();
        internal DbSet<ItemModelFeatureValue> ItemModelFeatureValues => Set<ItemModelFeatureValue>();
        internal DbSet<Supplier> Suppliers => Set<Supplier>();
        internal DbSet<ItemMaterial> ItemMaterials => Set<ItemMaterial>();
        internal DbSet<ItemSerie> ItemSeries => Set<ItemSerie>();
        internal DbSet<ItemFeature> ItemFeatures => Set<ItemFeature>();
        internal DbSet<ItemFeatureValue> ItemFeatureValues => Set<ItemFeatureValue>();
        internal DbSet<ItemFeatureToValue> ItemFeatureToValues => Set<ItemFeatureToValue>();
        internal DbSet<ItemSerieToFeatureAndValue> ItemSerieToFeatureAndValues => Set<ItemSerieToFeatureAndValue>();
        #endregion

        #region Sales
        internal DbSet<Customer> Customers => Set<Customer>();
        internal DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        internal DbSet<SaleOrderLine> SalesOrderLines => Set<SaleOrderLine>();
        #endregion

        #region Accouting
        internal DbSet<Account> Accounts => Set<Account>();
        internal DbSet<SalePayment> SalePayments => Set<SalePayment>();
        #endregion

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

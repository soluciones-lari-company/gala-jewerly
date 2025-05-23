using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Application.Common.Interfaces
{
    public interface IJewerlyDbContext
    {
        DbSet<Account> Accounts { get; }
        DbSet<Customer> Customers { get; }
        DbSet<ItemFeature> ItemFeatures { get; }
        DbSet<ItemFeatureToValue> ItemFeatureToValues { get; }
        DbSet<ItemFeatureValue> ItemFeatureValues { get; }
        DbSet<ItemMaterial> ItemMaterials { get; }
        DbSet<ItemModelFeature> ItemModelFeatures { get; }
        DbSet<ItemModelFeatureValue> ItemModelFeatureValues { get; }
        DbSet<ItemModel> ItemModels { get; }
        DbSet<ItemSerie> ItemSeries { get; }
        DbSet<ItemSerieToFeatureAndValue> ItemSerieToFeatureAndValues { get; }
        DbSet<SalePayment> SalePayments { get; }
        DbSet<SaleOrderLine> SalesOrderLines { get; }
        DbSet<SalesOrder> SalesOrders { get; }
        DbSet<Supplier> Suppliers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

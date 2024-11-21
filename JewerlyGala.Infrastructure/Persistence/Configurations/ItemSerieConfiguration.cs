using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemSerieConfiguration : IEntityTypeConfiguration<ItemSerie>
    {
        public void Configure(EntityTypeBuilder<ItemSerie> builder)
        {
            builder.ToTable("ItemSerie");

            builder.HasKey(e => e.Id).HasName("PK_ItemSerie_id");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.SerieCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(e => e.MaterialId)
                .IsRequired();

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.Property(e => e.QuantitySold)
                .IsRequired();

            builder.Property(e => e.QuantityCommited)
                .IsRequired();

            builder.Property(e => e.QuantityFree)
                .IsRequired();

            builder.Property(e => e.SupplierId)
                .IsRequired();

            builder.Property(e => e.PurchaseUnitMeasure)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.PurchasePriceByUnitMeasure)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(e => e.PurchaseDate)
                .IsRequired();

            builder.Property(e => e.PurchaseUnitPrice)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(e => e.SalePercentRentability)
                .IsRequired();

            builder.Property(e => e.SaleUnitPrice)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.HasOne(t => t.SupplierNav)
                .WithMany(t => t.ItemSeriesNav)
                .HasForeignKey(t => t.SupplierId);

            builder.HasOne(t => t.ItemMaterialNav)
                .WithMany(t => t.ItemSeriesNav)
                .HasForeignKey(t => t.MaterialId);
        }
    }
}
using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    internal class SaleOrderLineConfiguration : IEntityTypeConfiguration<SaleOrderLine>
    {
        public void Configure(EntityTypeBuilder<SaleOrderLine> builder)
        {
            builder.ToTable("SaleOrderLine");

            builder.HasKey(e => e.Id).HasName("PK_SaleOrderLine_id");

            builder.Property(e => e.Id)
                    .UseIdentityColumn();

            builder.Property(t => t.NumLine)
               .IsRequired();

            builder.Property(t => t.ItemSerieId)
               .IsRequired();

            builder.Property(t => t.SerieCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Descripcion)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(t => t.Quantity)
                .IsRequired();

            builder.Property(e => e.UnitPrice)
               .HasColumnType("decimal(10, 2)")
               .IsRequired();

            builder.Property(e => e.SubTotal)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(e => e.DiscountPercentaje)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(e => e.DiscountTotal)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(e => e.UnitPriceFinal)
               .HasColumnType("decimal(10, 2)")
               .IsRequired();

            builder.HasOne(t => t.SaleOrderNavigation)
                .WithMany(t => t.SaleOrderLinesNavigation)
                .HasForeignKey(t => t.SalesOrderId);
        }
    }
}
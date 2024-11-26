using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.ToTable("SalesOrder");

            builder.HasKey(e => e.Id).HasName("PK_SalesOrder_id");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(t => t.IdCustomer)
               .IsRequired();

            builder.Property(t => t.Date)
               .IsRequired();

            builder.Property(t => t.PaymentTerms)
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(t => t.PaymentMethod)
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(t => t.PaymentConditions)
                .HasMaxLength(15)
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

            builder.Property(t => t.Zone)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(t => t.CustomerNavigation)
                .WithMany(t => t.SalesOrdersNavigation)
                .HasForeignKey(t => t.IdCustomer);
        }
    }
}
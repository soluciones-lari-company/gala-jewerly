using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class SalePaymentOrderConfiguration : IEntityTypeConfiguration<SalePaymentOrder>
    {
        public void Configure(EntityTypeBuilder<SalePaymentOrder> builder)
        {
            builder.ToTable("SalePaymentOrder");

            builder.HasKey(e => e.Id).HasName("PK_SalePaymentOrder_id");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(t => t.IdSaleOrder)
                .IsRequired();

            builder.Property(t => t.IdSalePayment)
                .IsRequired();

            builder.HasOne(t => t.SaleOrder)
                .WithMany(t => t.SalePaymentsApplied)
                .HasForeignKey(t => t.IdSaleOrder);

            builder.HasOne(t => t.PaymentHeader)
                .WithMany(t => t.SalePaymentsApplied)
                .HasForeignKey(t => t.IdSalePayment);
        }
    }
}
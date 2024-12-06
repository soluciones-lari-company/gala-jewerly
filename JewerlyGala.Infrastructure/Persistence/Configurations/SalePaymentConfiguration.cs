using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class SalePaymentConfiguration : IEntityTypeConfiguration<SalePayment>
    {
        public void Configure(EntityTypeBuilder<SalePayment> builder)
        {
            builder.ToTable("SalePayment");

            builder.HasKey(e => e.Id).HasName("PK_SalePayment_id");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(t => t.Date)
                .IsRequired();

            builder.Property(t => t.IdCustomer)
                .IsRequired();

            builder.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.TotalApplied)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(e => e.TotalFree)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(t => t.PaymentMethod)
                .HasMaxLength(2)
                .IsRequired();

            builder.HasOne(t => t.Customer)
                .WithMany(t => t.Payments)
                .HasForeignKey(t => t.IdCustomer);
        }
    }
}
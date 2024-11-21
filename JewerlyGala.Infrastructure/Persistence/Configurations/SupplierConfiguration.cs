using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");

            builder.HasKey(e => e.Id).HasName("PK_Supplier_id");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(t => t.SupplierName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
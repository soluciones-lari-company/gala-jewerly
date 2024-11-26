using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(e => e.Id).HasName("PK_Customer_id");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
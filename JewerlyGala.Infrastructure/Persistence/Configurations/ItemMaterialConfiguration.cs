using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemMaterialConfiguration : IEntityTypeConfiguration<ItemMaterial>
    {
        public void Configure(EntityTypeBuilder<ItemMaterial> builder)
        {
            builder.ToTable("ItemMaterial");

            builder.HasKey(e => e.Id).HasName("PK_ItemMaterial_id");

            builder.Property(e => e.Id)
                    .UseIdentityColumn();

            builder.Property(t => t.MaterialName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
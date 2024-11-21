using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemFeatureValueConfiguration : IEntityTypeConfiguration<ItemFeatureValue>
    {
        public void Configure(EntityTypeBuilder<ItemFeatureValue> builder)
        {
            builder.ToTable("ItemFeatureValue");

            builder.HasKey(e => e.Id).HasName("PK_ItemFeatureValue_id");

            builder.Property(e => e.Id)
                    .UseIdentityColumn();

            builder.Property(t => t.ValueName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
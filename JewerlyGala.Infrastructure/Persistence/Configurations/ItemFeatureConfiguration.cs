using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemFeatureConfiguration : IEntityTypeConfiguration<ItemFeature>
    {
        public void Configure(EntityTypeBuilder<ItemFeature> builder)
        {
            builder.ToTable("ItemFeature");

            builder.HasKey(e => e.Id).HasName("PK_ItemFeature_id");

            builder.Property(e => e.Id)
                    .UseIdentityColumn();

            builder.Property(t => t.FeatureName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemSerieToFeatureAndValueConfiguration : IEntityTypeConfiguration<ItemSerieToFeatureAndValue>
    {
        public void Configure(EntityTypeBuilder<ItemSerieToFeatureAndValue> builder)
        {
            builder.ToTable("ItemSerieToFeatureAndValue");

            builder.HasKey(e => new { e.ItemSerieId, e.ItemFeatureToValueId }).HasName("PK_ItemSerieToFeatureAndValue_id");

            builder.Property(e => e.ItemSerieId)
                .IsRequired();

            builder.Property(e => e.ItemFeatureToValueId)
                .IsRequired();

            builder.HasOne(t => t.ItemFeatureToValueNav)
               .WithMany(t => t.ItemSerieToFeatureAndValueNav)
               .HasForeignKey(t => t.ItemFeatureToValueId);
        }
    }
}

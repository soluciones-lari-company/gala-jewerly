using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemFeatureToValueConfiguration : IEntityTypeConfiguration<ItemFeatureToValue>
    {
        public void Configure(EntityTypeBuilder<ItemFeatureToValue> builder)
        {
            builder.ToTable("ItemFeatureToValue");

            builder.HasKey(e => e.Id).HasName("PK_ItemFeatureToValue_id");

            builder.Property(e => e.Id)
                    .UseIdentityColumn();

            builder.Property(t => t.FeatureId)
                .IsRequired();

            builder.Property(t => t.ValueId)
                .IsRequired();

            builder.HasOne(t => t.FeatureNav)
                .WithMany(t => t.ItemFeatureToValueNav)
                .HasForeignKey(t => t.FeatureId);

            builder.HasOne(t => t.FeatureValueNav)
                .WithMany(t => t.ItemFeatureToValueNav)
                .HasForeignKey(t => t.ValueId);
        }
    }
}
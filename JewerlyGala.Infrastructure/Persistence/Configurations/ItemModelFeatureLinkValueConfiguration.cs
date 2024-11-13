using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemModelFeatureLinkValueConfiguration : IEntityTypeConfiguration<ItemModelFeatureLinkValue>
    {
        public void Configure(EntityTypeBuilder<ItemModelFeatureLinkValue> builder)
        {
            builder.ToTable("ItemModelFeatureLinkValue");

            builder.HasKey(e => new { e.IdValue, e.IdFeature }).HasName("PK_ItemModelFeatureLinkValue_id");

            builder.HasOne(t => t.Feature)
               .WithMany(t => t.Values)
               .HasForeignKey(t => t.IdFeature);

            builder.HasOne(t => t.Value)
               .WithMany(t => t.Features)
               .HasForeignKey(t => t.IdValue);
        }
    }
}

using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemModelLinkFeatureConfiguration : IEntityTypeConfiguration<ItemModelLinkFeature>
    {
        public void Configure(EntityTypeBuilder<ItemModelLinkFeature> builder)
        {
            builder.ToTable("ItemModelLinkFeature");

            builder.HasKey(e => new { e.IdModel, e.IdFeature }).HasName("PK_ItemModelLinkFeature_id");

            builder.HasOne(t => t.Model)
               .WithMany(t => t.Features)
               .HasForeignKey(t => t.IdModel);

            builder.HasOne(t => t.Feature)
               .WithMany(t => t.Models)
               .HasForeignKey(t => t.IdFeature);
        }
    }
}

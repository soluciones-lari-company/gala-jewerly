using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemModelFeatureValueConfiguration : IEntityTypeConfiguration<ItemModelFeatureValue>
    {
        public void Configure(EntityTypeBuilder<ItemModelFeatureValue> builder)
        {
            builder.ToTable("ItemModelFeatureValue");

            builder.HasKey(e => e.Id).HasName("PK_ItemModelFeatureValue_id");

            builder.Property(e => e.Id).UseIdentityColumn();

            //builder.Property(t => t.IdFeature).IsRequired();

            builder.Property(t => t.ValueDetails)
                .HasMaxLength(200)
                .IsRequired();

            //builder.HasOne(t => t.Feature)
            //    .WithMany(t => t.Values)
            //    .HasForeignKey(t => t.IdFeature);
        }
    }
}

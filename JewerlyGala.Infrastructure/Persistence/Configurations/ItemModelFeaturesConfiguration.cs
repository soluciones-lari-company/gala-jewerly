using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemModelFeaturesConfiguration : IEntityTypeConfiguration<ItemModelFeature>
    {
        public void Configure(EntityTypeBuilder<ItemModelFeature> builder)
        {
            builder.ToTable("ItemModelFeature");

            builder.HasKey(e => e.Id).HasName("PK_ItemModelFeature_id");

            builder.Property(e => e.Id).UseIdentityColumn();

            //builder.Property(t => t.IdModel).IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            //builder.HasOne(t => t.Model)
            //    .WithMany(t => t.Features)
            //    .HasForeignKey(t => t.IdModel);
        }
    }
}

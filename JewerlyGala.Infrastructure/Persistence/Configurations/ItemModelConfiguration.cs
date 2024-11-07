using JewerlyGala.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JewerlyGala.Infrastructure.Persistence.Configurations
{
    public class ItemModelConfiguration : IEntityTypeConfiguration<ItemModel>
    {
        public void Configure(EntityTypeBuilder<ItemModel> builder)
        {
            builder.ToTable("ItemModel");

            builder.HasKey(e => e.Id).HasName("PK_ItemModel_id");

            builder.Property(e => e.Id)
                    .UseIdentityColumn();

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}

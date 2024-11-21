﻿// <auto-generated />
using System;
using JewerlyGala.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(JewerlyDbContext))]
    [Migration("20241120064214_seriesProps")]
    partial class seriesProps
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FeatureName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_ItemFeature_id");

                    b.ToTable("ItemFeature", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeatureToValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int>("ValueId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_ItemFeatureToValue_id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ValueId");

                    b.ToTable("ItemFeatureToValue", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeatureValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ValueName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_ItemFeatureValue_id");

                    b.ToTable("ItemFeatureValue", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_ItemMaterial_id");

                    b.ToTable("ItemMaterial", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_ItemModel_id");

                    b.ToTable("ItemModel", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_ItemModelFeature_id");

                    b.ToTable("ItemModelFeature", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelFeatureLinkValue", b =>
                {
                    b.Property<int>("IdValue")
                        .HasColumnType("int");

                    b.Property<int>("IdFeature")
                        .HasColumnType("int");

                    b.HasKey("IdValue", "IdFeature")
                        .HasName("PK_ItemModelFeatureLinkValue_id");

                    b.HasIndex("IdFeature");

                    b.ToTable("ItemModelFeatureLinkValue", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelFeatureValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ValueDetails")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_ItemModelFeatureValue_id");

                    b.ToTable("ItemModelFeatureValue", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelLinkFeature", b =>
                {
                    b.Property<int>("IdModel")
                        .HasColumnType("int");

                    b.Property<int>("IdFeature")
                        .HasColumnType("int");

                    b.HasKey("IdModel", "IdFeature")
                        .HasName("PK_ItemModelLinkFeature_id");

                    b.HasIndex("IdFeature");

                    b.ToTable("ItemModelLinkFeature", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemSerie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("PurchaseDate")
                        .HasColumnType("date");

                    b.Property<decimal>("PurchasePriceByUnitMeasure")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("PurchaseUnitMeasure")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("PurchaseUnitPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("QuantityCommited")
                        .HasColumnType("int");

                    b.Property<int>("QuantityFree")
                        .HasColumnType("int");

                    b.Property<int>("QuantitySold")
                        .HasColumnType("int");

                    b.Property<int>("SalePercentRentability")
                        .HasColumnType("int");

                    b.Property<decimal>("SaleUnitPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("SerieCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_ItemSerie_id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("SupplierId");

                    b.ToTable("ItemSerie", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemSerieToFeatureAndValue", b =>
                {
                    b.Property<Guid>("ItemSerieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ItemFeatureToValueId")
                        .HasColumnType("int");

                    b.HasKey("ItemSerieId", "ItemFeatureToValueId")
                        .HasName("PK_ItemSerieToFeatureAndValue_id");

                    b.HasIndex("ItemFeatureToValueId");

                    b.ToTable("ItemSerieToFeatureAndValue", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK_Supplier_id");

                    b.ToTable("Supplier", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeatureToValue", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Entities.ItemFeature", "FeatureNav")
                        .WithMany("ItemFeatureToValueNav")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JewerlyGala.Domain.Entities.ItemFeatureValue", "FeatureValueNav")
                        .WithMany("ItemFeatureToValueNav")
                        .HasForeignKey("ValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeatureNav");

                    b.Navigation("FeatureValueNav");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelFeatureLinkValue", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Entities.ItemModelFeature", "Feature")
                        .WithMany("Values")
                        .HasForeignKey("IdFeature")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JewerlyGala.Domain.Entities.ItemModelFeatureValue", "Value")
                        .WithMany("Features")
                        .HasForeignKey("IdValue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("Value");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelLinkFeature", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Entities.ItemModelFeature", "Feature")
                        .WithMany("Models")
                        .HasForeignKey("IdFeature")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JewerlyGala.Domain.Entities.ItemModel", "Model")
                        .WithMany("Features")
                        .HasForeignKey("IdModel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemSerie", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Entities.ItemMaterial", "ItemMaterialNav")
                        .WithMany("ItemSeriesNav")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JewerlyGala.Domain.Entities.Supplier", "SupplierNav")
                        .WithMany("ItemSeriesNav")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemMaterialNav");

                    b.Navigation("SupplierNav");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemSerieToFeatureAndValue", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Entities.ItemFeatureToValue", "ItemFeatureToValueNav")
                        .WithMany("ItemSerieToFeatureAndValueNav")
                        .HasForeignKey("ItemFeatureToValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemFeatureToValueNav");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JewerlyGala.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JewerlyGala.Domain.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeature", b =>
                {
                    b.Navigation("ItemFeatureToValueNav");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeatureToValue", b =>
                {
                    b.Navigation("ItemSerieToFeatureAndValueNav");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemFeatureValue", b =>
                {
                    b.Navigation("ItemFeatureToValueNav");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemMaterial", b =>
                {
                    b.Navigation("ItemSeriesNav");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModel", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelFeature", b =>
                {
                    b.Navigation("Models");

                    b.Navigation("Values");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.ItemModelFeatureValue", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("JewerlyGala.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("ItemSeriesNav");
                });
#pragma warning restore 612, 618
        }
    }
}

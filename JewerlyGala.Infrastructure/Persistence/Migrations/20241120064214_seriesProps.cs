using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seriesProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFeature_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemFeatureValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFeatureValue_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMaterial_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemFeatureToValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    ValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFeatureToValue_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemFeatureToValue_ItemFeatureValue_ValueId",
                        column: x => x.ValueId,
                        principalTable: "ItemFeatureValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemFeatureToValue_ItemFeature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "ItemFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSerie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    SerieCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantitySold = table.Column<int>(type: "int", nullable: false),
                    QuantityCommited = table.Column<int>(type: "int", nullable: false),
                    QuantityFree = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseUnitMeasure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PurchasePriceByUnitMeasure = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PurchaseUnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SalePercentRentability = table.Column<int>(type: "int", nullable: false),
                    SaleUnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSerie_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSerie_ItemMaterial_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ItemMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSerie_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSerieToFeatureAndValue",
                columns: table => new
                {
                    ItemSerieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemFeatureToValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSerieToFeatureAndValue_id", x => new { x.ItemSerieId, x.ItemFeatureToValueId });
                    table.ForeignKey(
                        name: "FK_ItemSerieToFeatureAndValue_ItemFeatureToValue_ItemFeatureToValueId",
                        column: x => x.ItemFeatureToValueId,
                        principalTable: "ItemFeatureToValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemFeatureToValue_FeatureId",
                table: "ItemFeatureToValue",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemFeatureToValue_ValueId",
                table: "ItemFeatureToValue",
                column: "ValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSerie_MaterialId",
                table: "ItemSerie",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSerie_SupplierId",
                table: "ItemSerie",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSerieToFeatureAndValue_ItemFeatureToValueId",
                table: "ItemSerieToFeatureAndValue",
                column: "ItemFeatureToValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSerie");

            migrationBuilder.DropTable(
                name: "ItemSerieToFeatureAndValue");

            migrationBuilder.DropTable(
                name: "ItemMaterial");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "ItemFeatureToValue");

            migrationBuilder.DropTable(
                name: "ItemFeatureValue");

            migrationBuilder.DropTable(
                name: "ItemFeature");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    public partial class itemModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemModelFeature_ItemModel_IdModel",
                table: "ItemModelFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemModelFeatureValue_ItemModelFeature_IdFeature",
                table: "ItemModelFeatureValue");

            migrationBuilder.DropIndex(
                name: "IX_ItemModelFeatureValue_IdFeature",
                table: "ItemModelFeatureValue");

            migrationBuilder.DropIndex(
                name: "IX_ItemModelFeature_IdModel",
                table: "ItemModelFeature");

            migrationBuilder.DropColumn(
                name: "IdFeature",
                table: "ItemModelFeatureValue");

            migrationBuilder.CreateTable(
                name: "ItemModelFeatureLinkValue",
                columns: table => new
                {
                    IdFeature = table.Column<int>(type: "int", nullable: false),
                    IdValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModelFeatureLinkValue_id", x => new { x.IdValue, x.IdFeature });
                    table.ForeignKey(
                        name: "FK_ItemModelFeatureLinkValue_ItemModelFeature_IdFeature",
                        column: x => x.IdFeature,
                        principalTable: "ItemModelFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemModelFeatureLinkValue_ItemModelFeatureValue_IdValue",
                        column: x => x.IdValue,
                        principalTable: "ItemModelFeatureValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemModelLinkFeature",
                columns: table => new
                {
                    IdModel = table.Column<int>(type: "int", nullable: false),
                    IdFeature = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModelLinkFeature_id", x => new { x.IdModel, x.IdFeature });
                    table.ForeignKey(
                        name: "FK_ItemModelLinkFeature_ItemModel_IdModel",
                        column: x => x.IdModel,
                        principalTable: "ItemModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemModelLinkFeature_ItemModelFeature_IdFeature",
                        column: x => x.IdFeature,
                        principalTable: "ItemModelFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemModelFeatureLinkValue_IdFeature",
                table: "ItemModelFeatureLinkValue",
                column: "IdFeature");

            migrationBuilder.CreateIndex(
                name: "IX_ItemModelLinkFeature_IdFeature",
                table: "ItemModelLinkFeature",
                column: "IdFeature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemModelFeatureLinkValue");

            migrationBuilder.DropTable(
                name: "ItemModelLinkFeature");

            migrationBuilder.AddColumn<int>(
                name: "IdFeature",
                table: "ItemModelFeatureValue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemModelFeatureValue_IdFeature",
                table: "ItemModelFeatureValue",
                column: "IdFeature");

            migrationBuilder.CreateIndex(
                name: "IX_ItemModelFeature_IdModel",
                table: "ItemModelFeature",
                column: "IdModel");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModelFeature_ItemModel_IdModel",
                table: "ItemModelFeature",
                column: "IdModel",
                principalTable: "ItemModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModelFeatureValue_ItemModelFeature_IdFeature",
                table: "ItemModelFeatureValue",
                column: "IdFeature",
                principalTable: "ItemModelFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

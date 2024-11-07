using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModel_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemModelFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdModel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModelFeature_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemModelFeature_ItemModel_IdModel",
                        column: x => x.IdModel,
                        principalTable: "ItemModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemModelFeatureValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueDetails = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdFeature = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModelFeatureValue_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemModelFeatureValue_ItemModelFeature_IdFeature",
                        column: x => x.IdFeature,
                        principalTable: "ItemModelFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemModelFeature_IdModel",
                table: "ItemModelFeature",
                column: "IdModel");

            migrationBuilder.CreateIndex(
                name: "IX_ItemModelFeatureValue_IdFeature",
                table: "ItemModelFeatureValue",
                column: "IdFeature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemModelFeatureValue");

            migrationBuilder.DropTable(
                name: "ItemModelFeature");

            migrationBuilder.DropTable(
                name: "ItemModel");
        }
    }
}

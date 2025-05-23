using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class lineType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeLine",
                table: "SaleOrderLine",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeLine",
                table: "SaleOrderLine");
        }
    }
}

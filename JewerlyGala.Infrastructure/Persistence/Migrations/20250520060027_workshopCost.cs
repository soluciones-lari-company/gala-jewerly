using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class workshopCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WorkshopCost",
                table: "SalesOrder",
                type: "decimal(10,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkshopCost",
                table: "SalesOrder");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewerlyGala.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PaymentMethodAcceptable = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalePayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCustomer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAccount = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSaleOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    PaymentMethod = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePayment_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalePayment_Account_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalePayment_Customer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalePayment_SalesOrder_IdSaleOrder",
                        column: x => x.IdSaleOrder,
                        principalTable: "SalesOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_IdAccount",
                table: "SalePayment",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_IdCustomer",
                table: "SalePayment",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_IdSaleOrder",
                table: "SalePayment",
                column: "IdSaleOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalePayment");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}

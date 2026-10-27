using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryMovementsAndTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovementTypes",
                columns: table => new
                {
                    IdMovementType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsPositive = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementTypes", x => x.IdMovementType);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovements",
                columns: table => new
                {
                    IdMovement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMovementType = table.Column<int>(type: "int", nullable: false),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    IdStore = table.Column<int>(type: "int", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovements", x => x.IdMovement);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_MovementTypes_IdMovementType",
                        column: x => x.IdMovementType,
                        principalTable: "MovementTypes",
                        principalColumn: "IdMovementType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Stores_IdStore",
                        column: x => x.IdStore,
                        principalTable: "Stores",
                        principalColumn: "IdStore",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_IdMovementType",
                table: "InventoryMovements",
                column: "IdMovementType");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_IdProduct",
                table: "InventoryMovements",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_IdStore",
                table: "InventoryMovements",
                column: "IdStore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryMovements");

            migrationBuilder.DropTable(
                name: "MovementTypes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yenilen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "StoreOwners");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Stores",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Stores");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "StoreOwners",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

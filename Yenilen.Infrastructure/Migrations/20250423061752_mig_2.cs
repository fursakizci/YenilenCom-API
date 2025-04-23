using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yenilen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Stores_StoreId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Stores_StoreId",
                table: "Images",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Stores_StoreId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Stores_StoreId",
                table: "Images",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }
    }
}

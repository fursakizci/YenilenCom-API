using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yenilen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreOwners_AspNetUsers_AppUserId",
                table: "StoreOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StoreOwners_StoreOwnerId",
                table: "Stores");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StoreOwners",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "StoreOwners",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreOwners_AspNetUsers_AppUserId",
                table: "StoreOwners",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_StoreOwners_StoreOwnerId",
                table: "Stores",
                column: "StoreOwnerId",
                principalTable: "StoreOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreOwners_AspNetUsers_AppUserId",
                table: "StoreOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StoreOwners_StoreOwnerId",
                table: "Stores");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StoreOwners",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "StoreOwners",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreOwners_AspNetUsers_AppUserId",
                table: "StoreOwners",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_StoreOwners_StoreOwnerId",
                table: "Stores",
                column: "StoreOwnerId",
                principalTable: "StoreOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

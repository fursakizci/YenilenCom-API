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
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "StaffMembers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "StaffMembers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_Email",
                table: "StaffMembers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_Email",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "StaffMembers");
        }
    }
}

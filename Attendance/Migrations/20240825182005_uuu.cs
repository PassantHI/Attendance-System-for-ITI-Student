using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance.Migrations
{
    /// <inheritdoc />
    public partial class uuu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HRs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HRs_UserId",
                table: "HRs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRs_Users_UserId",
                table: "HRs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRs_Users_UserId",
                table: "HRs");

            migrationBuilder.DropIndex(
                name: "IX_HRs_UserId",
                table: "HRs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HRs");
        }
    }
}

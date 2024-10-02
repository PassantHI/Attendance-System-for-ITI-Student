using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance.Migrations
{
    /// <inheritdoc />
    public partial class mmmn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_StdAttends_StdId",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "StdAttendStdId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "studentId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StdAttendStdId",
                table: "Attendances",
                column: "StdAttendStdId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_studentId",
                table: "Attendances",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_StdAttends_StdAttendStdId",
                table: "Attendances",
                column: "StdAttendStdId",
                principalTable: "StdAttends",
                principalColumn: "StdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_studentId",
                table: "Attendances",
                column: "studentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_StdAttends_StdAttendStdId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_studentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StdAttendStdId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_studentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StdAttendStdId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "Attendances");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_StdAttends_StdId",
                table: "Attendances",
                column: "StdId",
                principalTable: "StdAttends",
                principalColumn: "StdId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

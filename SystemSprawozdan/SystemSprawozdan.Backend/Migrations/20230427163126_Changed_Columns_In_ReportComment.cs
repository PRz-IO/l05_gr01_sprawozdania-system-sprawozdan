using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Columns_In_ReportComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_Student_StudentId",
                table: "ReportComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_Teacher_TeacherId",
                table: "ReportComment");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "ReportComment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StudentReportId",
                table: "ReportComment",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ReportComment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment",
                column: "StudentReportId",
                principalTable: "StudentReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_Student_StudentId",
                table: "ReportComment",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_Teacher_TeacherId",
                table: "ReportComment",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_Student_StudentId",
                table: "ReportComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_Teacher_TeacherId",
                table: "ReportComment");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "ReportComment",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentReportId",
                table: "ReportComment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "ReportComment",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ReportComment",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment",
                column: "StudentReportId",
                principalTable: "StudentReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_Student_StudentId",
                table: "ReportComment",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_Teacher_TeacherId",
                table: "ReportComment",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Nullable_Requirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StudentReport",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ReportComment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StudentReportId",
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
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StudentReport",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
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
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportComment_StudentReport_StudentReportId",
                table: "ReportComment",
                column: "StudentReportId",
                principalTable: "StudentReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Bonus_bagno_migracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentReportFile_StudentReport_StudentReportId",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "File",
                table: "StudentReportFile");

            migrationBuilder.AddColumn<bool>(
                name: "IsIndividual",
                table: "SubjectSubgroup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "StudentReportId",
                table: "StudentReportFile",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "StudentReportFile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "StudentReportFile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "StudentReportFile",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentReportFile_StudentReport_StudentReportId",
                table: "StudentReportFile",
                column: "StudentReportId",
                principalTable: "StudentReport",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentReportFile_StudentReport_StudentReportId",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "IsIndividual",
                table: "SubjectSubgroup");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "StudentReportFile");

            migrationBuilder.AlterColumn<int>(
                name: "StudentReportId",
                table: "StudentReportFile",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "StudentReportFile",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentReportFile_StudentReport_StudentReportId",
                table: "StudentReportFile",
                column: "StudentReportId",
                principalTable: "StudentReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

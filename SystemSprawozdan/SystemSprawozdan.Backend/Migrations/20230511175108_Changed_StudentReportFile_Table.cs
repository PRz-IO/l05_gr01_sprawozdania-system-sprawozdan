using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Changed_StudentReportFile_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "StudentReportFile");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "StudentReportFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "StudentReportFile",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}

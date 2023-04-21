using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_Final_Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentReportFile_ReportTopic_ReportTopicId",
                table: "StudentReportFile");

            migrationBuilder.DropIndex(
                name: "IX_StudentReportFile_ReportTopicId",
                table: "StudentReportFile");

            migrationBuilder.DropColumn(
                name: "ReportTopicId",
                table: "StudentReportFile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportTopicId",
                table: "StudentReportFile",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentReportFile_ReportTopicId",
                table: "StudentReportFile",
                column: "ReportTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentReportFile_ReportTopic_ReportTopicId",
                table: "StudentReportFile",
                column: "ReportTopicId",
                principalTable: "ReportTopic",
                principalColumn: "Id");
        }
    }
}

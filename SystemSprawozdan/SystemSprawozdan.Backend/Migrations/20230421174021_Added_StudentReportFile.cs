using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_StudentReportFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentReportFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    File = table.Column<byte[]>(type: "bytea", nullable: false),
                    StudentReportId = table.Column<int>(type: "integer", nullable: false),
                    ReportTopicId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentReportFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentReportFile_ReportTopic_ReportTopicId",
                        column: x => x.ReportTopicId,
                        principalTable: "ReportTopic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentReportFile_StudentReport_StudentReportId",
                        column: x => x.StudentReportId,
                        principalTable: "StudentReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentReportFile_ReportTopicId",
                table: "StudentReportFile",
                column: "ReportTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReportFile_StudentReportId",
                table: "StudentReportFile",
                column: "StudentReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentReportFile");
        }
    }
}

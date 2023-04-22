using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_StudentReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectSubgroup_Student_StudentId",
                table: "StudentSubjectSubgroup");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentSubjectSubgroup",
                newName: "StudentsId");

            migrationBuilder.CreateTable(
                name: "StudentReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    ReportTopicId = table.Column<int>(type: "integer", nullable: false),
                    SubjectSubgroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentReport_ReportTopic_ReportTopicId",
                        column: x => x.ReportTopicId,
                        principalTable: "ReportTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentReport_SubjectSubgroup_SubjectSubgroupId",
                        column: x => x.SubjectSubgroupId,
                        principalTable: "SubjectSubgroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentReport_ReportTopicId",
                table: "StudentReport",
                column: "ReportTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReport_SubjectSubgroupId",
                table: "StudentReport",
                column: "SubjectSubgroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectSubgroup_Student_StudentsId",
                table: "StudentSubjectSubgroup",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectSubgroup_Student_StudentsId",
                table: "StudentSubjectSubgroup");

            migrationBuilder.DropTable(
                name: "StudentReport");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentSubjectSubgroup",
                newName: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectSubgroup_Student_StudentId",
                table: "StudentSubjectSubgroup",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

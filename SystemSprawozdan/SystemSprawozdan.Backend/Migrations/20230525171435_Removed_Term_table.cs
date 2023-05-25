using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Term_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    MajorCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MajorId = table.Column<int>(type: "integer", nullable: false),
                    Term = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GroupType = table.Column<string>(type: "text", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGroup_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGroup_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTopic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubjectGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTopic_SubjectGroup_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectSubgroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SubjectGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsIndividual = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSubgroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectSubgroup_SubjectGroup_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ReportTopicId = table.Column<int>(type: "integer", nullable: false),
                    SubjectSubgroupId = table.Column<int>(type: "integer", nullable: false),
                    ToCheck = table.Column<bool>(type: "boolean", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "StudentSubjectSubgroup",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "integer", nullable: false),
                    SubjectSubgroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectSubgroup", x => new { x.StudentsId, x.SubjectSubgroupId });
                    table.ForeignKey(
                        name: "FK_StudentSubjectSubgroup_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjectSubgroup_SubjectSubgroup_SubjectSubgroupId",
                        column: x => x.SubjectSubgroupId,
                        principalTable: "SubjectSubgroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    StudentReportId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportComment_StudentReport_StudentReportId",
                        column: x => x.StudentReportId,
                        principalTable: "StudentReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportComment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportComment_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentReportFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    StoredFileName = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    StudentReportId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentReportFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentReportFile_StudentReport_StudentReportId",
                        column: x => x.StudentReportId,
                        principalTable: "StudentReport",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportComment_StudentId",
                table: "ReportComment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportComment_StudentReportId",
                table: "ReportComment",
                column: "StudentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportComment_TeacherId",
                table: "ReportComment",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTopic_SubjectGroupId",
                table: "ReportTopic",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReport_ReportTopicId",
                table: "StudentReport",
                column: "ReportTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReport_SubjectSubgroupId",
                table: "StudentReport",
                column: "SubjectSubgroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReportFile_StudentReportId",
                table: "StudentReportFile",
                column: "StudentReportId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectSubgroup_SubjectSubgroupId",
                table: "StudentSubjectSubgroup",
                column: "SubjectSubgroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_MajorId",
                table: "Subject",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroup_SubjectId",
                table: "SubjectGroup",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroup_TeacherId",
                table: "SubjectGroup",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSubgroup_SubjectGroupId",
                table: "SubjectSubgroup",
                column: "SubjectGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ReportComment");

            migrationBuilder.DropTable(
                name: "StudentReportFile");

            migrationBuilder.DropTable(
                name: "StudentSubjectSubgroup");

            migrationBuilder.DropTable(
                name: "StudentReport");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "ReportTopic");

            migrationBuilder.DropTable(
                name: "SubjectSubgroup");

            migrationBuilder.DropTable(
                name: "SubjectGroup");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Major");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Changed_TeacherSubjectGroup_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectGroupTeacher");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SubjectGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroup_TeacherId",
                table: "SubjectGroup",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectGroup_Teacher_TeacherId",
                table: "SubjectGroup",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectGroup_Teacher_TeacherId",
                table: "SubjectGroup");

            migrationBuilder.DropIndex(
                name: "IX_SubjectGroup_TeacherId",
                table: "SubjectGroup");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SubjectGroup");

            migrationBuilder.CreateTable(
                name: "SubjectGroupTeacher",
                columns: table => new
                {
                    SubjectGroupsId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroupTeacher", x => new { x.SubjectGroupsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SubjectGroupTeacher_SubjectGroup_SubjectGroupsId",
                        column: x => x.SubjectGroupsId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectGroupTeacher_Teacher_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroupTeacher_TeachersId",
                table: "SubjectGroupTeacher",
                column: "TeachersId");
        }
    }
}

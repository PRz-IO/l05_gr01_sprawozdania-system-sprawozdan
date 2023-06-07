using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class XD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectSubgroup_SubjectSubgroup_SubjectSubgroupId",
                table: "StudentSubjectSubgroup");

            migrationBuilder.RenameColumn(
                name: "SubjectSubgroupId",
                table: "StudentSubjectSubgroup",
                newName: "SubjectSubgroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectSubgroup_SubjectSubgroupId",
                table: "StudentSubjectSubgroup",
                newName: "IX_StudentSubjectSubgroup_SubjectSubgroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectSubgroup_SubjectSubgroup_SubjectSubgroupsId",
                table: "StudentSubjectSubgroup",
                column: "SubjectSubgroupsId",
                principalTable: "SubjectSubgroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectSubgroup_SubjectSubgroup_SubjectSubgroupsId",
                table: "StudentSubjectSubgroup");

            migrationBuilder.RenameColumn(
                name: "SubjectSubgroupsId",
                table: "StudentSubjectSubgroup",
                newName: "SubjectSubgroupId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectSubgroup_SubjectSubgroupsId",
                table: "StudentSubjectSubgroup",
                newName: "IX_StudentSubjectSubgroup_SubjectSubgroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectSubgroup_SubjectSubgroup_SubjectSubgroupId",
                table: "StudentSubjectSubgroup",
                column: "SubjectSubgroupId",
                principalTable: "SubjectSubgroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

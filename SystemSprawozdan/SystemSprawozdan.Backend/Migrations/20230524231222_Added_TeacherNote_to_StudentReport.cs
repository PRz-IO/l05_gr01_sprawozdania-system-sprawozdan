using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_TeacherNote_to_StudentReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "StudentReport",
                newName: "TeacherNote");

            migrationBuilder.AddColumn<string>(
                name: "StudentNote",
                table: "StudentReport",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentNote",
                table: "StudentReport");

            migrationBuilder.RenameColumn(
                name: "TeacherNote",
                table: "StudentReport",
                newName: "Note");
        }
    }
}
